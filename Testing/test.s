        .org    0

start:
        mov     #01000, sp
        mov     #00200, r6

        clr     r0
        clr     r1
        clr     r2
        clr     r3
        clr     r4
        clr     r5

loop:
        add     #1, r0
        add     r0, r1
        sub     r1, r2
        xor     r2, r3
        bis     r3, r4
        bic     r2, r4
        cmp     r4, r5
        beq     a
        inc     r5
a:
        mov     r0, (r6)+
        mov     r1, (r6)+
        mov     r2, (r6)+
        mov     r3, (r6)+

        cmp     r6, #00400
        blo     b
        mov     #00200, r6
b:
        mov     -(r6), r0
        mov     -(r6), r1
        mov     -(r6), r2
        mov     -(r6), r3

        tst     r0
        bpl     c
        neg     r0
c:
        tst     r1
        bmi     d
        com     r1
d:
        br      loop