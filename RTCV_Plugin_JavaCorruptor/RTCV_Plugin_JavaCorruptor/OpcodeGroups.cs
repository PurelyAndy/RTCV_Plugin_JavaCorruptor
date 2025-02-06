using System.Collections.Generic;

namespace Java_Corruptor;

public static class OpcodeGroups
{
    public static readonly HashSet<NoOperandOpcodes> PushInt =
    [
        NoOperandOpcodes.iconst_m1,
        NoOperandOpcodes.iconst_0,
        NoOperandOpcodes.iconst_1,
        NoOperandOpcodes.iconst_2,
        NoOperandOpcodes.iconst_3,
        NoOperandOpcodes.iconst_4,
        NoOperandOpcodes.iconst_5
    ];

    public static readonly HashSet<NoOperandOpcodes> PushLong =
    [
        NoOperandOpcodes.lconst_0,
        NoOperandOpcodes.lconst_1
    ];

    public static readonly HashSet<NoOperandOpcodes> PushFloat =
    [
        NoOperandOpcodes.fconst_0,
        NoOperandOpcodes.fconst_1,
        NoOperandOpcodes.fconst_2
    ];

    public static readonly HashSet<NoOperandOpcodes> PushDouble =
    [
        NoOperandOpcodes.dconst_0,
        NoOperandOpcodes.dconst_1
    ];

    public static readonly HashSet<NoOperandOpcodes> IntArrayLoad =
    [
        NoOperandOpcodes.iaload,
        // Java treats bytes, chars, and shorts as ints
        NoOperandOpcodes.baload,
        NoOperandOpcodes.caload,
        NoOperandOpcodes.saload
    ];

    public static readonly HashSet<NoOperandOpcodes> IntArrayStore =
    [
        NoOperandOpcodes.iastore,
        NoOperandOpcodes.bastore,
        NoOperandOpcodes.castore,
        NoOperandOpcodes.sastore
    ];

    public static readonly HashSet<NoOperandOpcodes> IntBinaryOps =
    [
        NoOperandOpcodes.iadd,
        NoOperandOpcodes.isub,
        NoOperandOpcodes.imul,
        NoOperandOpcodes.idiv,
        NoOperandOpcodes.irem,
        NoOperandOpcodes.iand,
        NoOperandOpcodes.ior,
        NoOperandOpcodes.ixor
    ];

    public static readonly HashSet<NoOperandOpcodes> LongBinaryOps =
    [
        NoOperandOpcodes.ladd,
        NoOperandOpcodes.lsub,
        NoOperandOpcodes.lmul,
        NoOperandOpcodes.ldiv,
        NoOperandOpcodes.lrem,
        NoOperandOpcodes.land,
        NoOperandOpcodes.lor,
        NoOperandOpcodes.lxor
    ];

    public static readonly HashSet<NoOperandOpcodes> FloatBinaryOps =
    [
        NoOperandOpcodes.fadd,
        NoOperandOpcodes.fsub,
        NoOperandOpcodes.fmul,
        NoOperandOpcodes.fdiv,
        NoOperandOpcodes.frem
    ];

    public static readonly HashSet<NoOperandOpcodes> DoubleBinaryOps =
    [
        NoOperandOpcodes.dadd,
        NoOperandOpcodes.dsub,
        NoOperandOpcodes.dmul,
        NoOperandOpcodes.ddiv,
        NoOperandOpcodes.drem
    ];

    // Special case: we group these together even though they're of different types
    // That way we can pair them with NOP for replacing
    public static readonly HashSet<NoOperandOpcodes> MixedUnaryOpsForNopping =
    [
        NoOperandOpcodes.fneg,
        NoOperandOpcodes.dneg
    ];
    
    public static readonly HashSet<NoOperandOpcodes> IntUnaryOps =
    [
        NoOperandOpcodes.ineg,
        NoOperandOpcodes.i2b,
        NoOperandOpcodes.i2c,
        NoOperandOpcodes.i2s,
        NoOperandOpcodes.ishl,
        NoOperandOpcodes.ishr,
        NoOperandOpcodes.iushr
    ];

    public static readonly HashSet<NoOperandOpcodes> LongUnaryOps =
    [
        NoOperandOpcodes.lneg,
        NoOperandOpcodes.lshl,
        NoOperandOpcodes.lshr,
        NoOperandOpcodes.lushr
    ];

    public static readonly HashSet<NoOperandOpcodes> FloatCompare =
    [
        NoOperandOpcodes.fcmpl,
        NoOperandOpcodes.fcmpg
    ];

    public static readonly HashSet<NoOperandOpcodes> DoubleCompare =
    [
        NoOperandOpcodes.dcmpl,
        NoOperandOpcodes.dcmpg
    ];
    
    public static readonly HashSet<NoOperandOpcodes>[] AllOpcodeGroups =
    [
        PushInt,
        PushLong,
        PushFloat,
        PushDouble,
        IntArrayLoad,
        IntArrayStore,
        IntBinaryOps,
        LongBinaryOps,
        FloatBinaryOps,
        DoubleBinaryOps,
        MixedUnaryOpsForNopping,
        IntUnaryOps,
        LongUnaryOps,
        FloatCompare,
        DoubleCompare
    ];
    
    public static readonly HashSet<NoOperandOpcodes> Unary =
    [
        .. MixedUnaryOpsForNopping,
        .. IntUnaryOps,
        .. LongUnaryOps
    ];
}