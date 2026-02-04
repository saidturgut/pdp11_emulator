using pdp1120.Executing.Components;

namespace pdp1120.Decoding;
using Signaling.Cycles;
using Signaling;
using Multiplexer;

public class Decoder : DecoderMux
{
    private readonly Dictionary<ushort, MicroCycle> FixedOpcodes = new()
    {
        {0x00, MicroCycle.HALT}, // HALT
        {0x01, MicroCycle.WAIT}, // WAIT
        {0x05, MicroCycle.RESET}, // RESET
    };
    
    public  Decoded Decode(ushort ir, TrapUnit trapUnit, Mode mode)
    {
        fzzz = (byte)(ir >> 12);
        zfzz = (byte)((ir & 0xF00) >> 8);

        // NOP
        if (ir == 0xA0) return ZERO_OPERAND(MicroCycle.EMPTY);

        // FIXED KERNEL INSTRUCTIONS
        if (FixedOpcodes.TryGetValue(ir, out var cycle))
        {
            if (mode == Mode.KERNEL) return ZERO_OPERAND(cycle);
            
            trapUnit.Request(TrapVector.PRIVILEGED_INSTRUCTION);
            return new Decoded();
        }

        // TRAP RELATED INSTRUCTIONS
        switch (ir)
        {
            case 2: case 6:
            {
                if (mode == Mode.KERNEL)
                    return RTI(ir);
                
                trapUnit.Request(TrapVector.PRIVILEGED_INSTRUCTION);
                return new Decoded();
            }
            case 3: return TRAP(TrapVector.BPT, trapUnit);
            case 4: return TRAP(TrapVector.IOT, trapUnit);
        }
        
        // TWO OPERAND INSTRUCTIONS
        if (fzzz is (>= 1 and <= 6) or (>= 9 and <= 0xE))
            return TWO_OPERAND(ir);
        
        // ONE OPERAND INSTRUCTIONS
        if ((fzzz is 0 or 8 && zfzz is >= 0xA and <= 0xC) 
            || ir >> 6 is 3 or 0x37)
            return ONE_OPERAND(ir);

        // ONE HALF OPERAND INSTRUCTIONS (NOT FULL EIS)
        if ((ir >> 9) >= 0x38 && (ir >> 9) <= 0x3C) 
            return ONE_HALF_OPERAND(ir);
        
        // DEBUGGER TRAPS
        switch (ir >> 8)
        {
            case 0x88 : return TRAP(TrapVector.EMT, trapUnit);
            case 0x89 : return TRAP(TrapVector.TRAP, trapUnit);
        }
        
        // CONTROL FLOW INSTRUCTIONS
        switch (ir >> 6)
        {
            case 0x1: return JMP(ir);
            case 0x34: return MARK(ir);
        }
        switch (ir >> 9)
        {
            case 0x4: return JSR(ir); 
            case 0x3F: return SOB(ir);
        }
        switch (ir >> 3)
        {
            case 0x10: return RTS(ir);
            case 0x13: return SPL(ir);
        }
        
        // BRANCH INSTRUCTIONS
        if (((fzzz == 0 && zfzz >= 1) || fzzz == 8) && zfzz <= 7)
            return BRANCH(ir);
        
        // PSW INSTRUCTIONS
        if((ir >> 5) == 0x5)
            return PSW(ir);
        
        // IF FALL THROUGH ->
        trapUnit.Request(TrapVector.ILLEGAL_INSTRUCTION);
        return new Decoded();
    }
}