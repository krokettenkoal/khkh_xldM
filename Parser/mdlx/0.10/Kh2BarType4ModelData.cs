// This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild

using System.Collections.Generic;

namespace Kaitai
{
    public partial class Kh2BarType4ModelData : KaitaiStruct
    {
        public static Kh2BarType4ModelData FromFile(string fileName)
        {
            return new Kh2BarType4ModelData(new KaitaiStream(fileName));
        }


        public enum ModelType
        {
            Map = 2,
            Object = 3,
            Shadow = 4,
        }

        public enum VifCmd
        {
            Nop = 0,
            Stcycl = 1,
            Offset = 2,
            Base = 3,
            Itop = 4,
            Stmod = 5,
            Mskpath3 = 6,
            Mark = 7,
            Flushe = 16,
            Flush = 17,
            Flusha = 19,
            Mscal = 20,
            Mscalf = 21,
            Mscnt = 23,
            Stmask = 32,
            Strow = 48,
            Stcol = 49,
            Mpg = 74,
            Direct = 80,
            Directhl = 81,
            UnmaskedS32 = 96,
            UnmaskedS16 = 97,
            UnmaskedS8 = 98,
            UnmaskedV232N100 = 100,
            UnmaskedV216 = 101,
            UnmaskedV28 = 102,
            UnmaskedV232N104 = 104,
            UnmaskedV316 = 105,
            UnmaskedV38 = 106,
            UnmaskedV432 = 108,
            UnmaskedV416 = 109,
            UnmaskedV48 = 110,
            UnmaskedV45 = 111,
            MaskedS32 = 112,
            MaskedS16 = 113,
            MaskedS8 = 114,
            MaskedV232N116 = 116,
            MaskedV216 = 117,
            MaskedV28 = 118,
            MaskedV232N120 = 120,
            MaskedV316 = 121,
            MaskedV38 = 122,
            MaskedV432 = 124,
            MaskedV416 = 125,
            MaskedV48 = 126,
            MaskedV45 = 127,
        }

        public enum DmaTagId
        {
            Refe = 0,
            Cnt = 1,
            Next = 2,
            Ref = 3,
            Refs = 4,
            Call = 5,
            Ret = 6,
            End = 7,
        }
        public Kh2BarType4ModelData(KaitaiStream p__io, KaitaiStruct p__parent = null, Kh2BarType4ModelData p__root = null) : base(p__io)
        {
            m_parent = p__parent;
            m_root = p__root ?? this;
            f_model = false;
            _read();
        }
        private void _read()
        {
            _hw = m_io.ReadBytes(144);
        }
        public partial class GifTag : KaitaiStruct
        {
            public static GifTag FromFile(string fileName)
            {
                return new GifTag(new KaitaiStream(fileName));
            }

            public GifTag(KaitaiStream p__io, Kh2BarType4ModelData.SourceChainDmaTag p__parent = null, Kh2BarType4ModelData p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _nloop = m_io.ReadBitsIntBe(15);
                _eop = m_io.ReadBitsIntBe(1) != 0;
                _skip = m_io.ReadBitsIntBe(31);
                _pre = m_io.ReadBitsIntBe(1) != 0;
                _prim = m_io.ReadBitsIntBe(10);
                _flg = m_io.ReadBitsIntBe(2);
                _nreg = m_io.ReadBitsIntBe(4);
                m_io.AlignToByte();
                _regs = m_io.ReadU8le();
            }
            private ulong _nloop;
            private bool _eop;
            private ulong _skip;
            private bool _pre;
            private ulong _prim;
            private ulong _flg;
            private ulong _nreg;
            private ulong _regs;
            private Kh2BarType4ModelData m_root;
            private Kh2BarType4ModelData.SourceChainDmaTag m_parent;
            public ulong Nloop { get { return _nloop; } }
            public bool Eop { get { return _eop; } }
            public ulong Skip { get { return _skip; } }
            public bool Pre { get { return _pre; } }
            public ulong Prim { get { return _prim; } }
            public ulong Flg { get { return _flg; } }
            public ulong Nreg { get { return _nreg; } }
            public ulong Regs { get { return _regs; } }
            public Kh2BarType4ModelData M_Root { get { return m_root; } }
            public Kh2BarType4ModelData.SourceChainDmaTag M_Parent { get { return m_parent; } }
        }
        public partial class DmaChainMap : KaitaiStruct
        {
            public static DmaChainMap FromFile(string fileName)
            {
                return new DmaChainMap(new KaitaiStream(fileName));
            }

            public DmaChainMap(KaitaiStream p__io, Kh2BarType4ModelData.MapDesc p__parent = null, Kh2BarType4ModelData p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_dmaTags = false;
                _read();
            }
            private void _read()
            {
                _dmaTagOff = m_io.ReadU4le();
                _textureIdx = m_io.ReadU4le();
                _unk1 = m_io.ReadU4le();
                _unk2 = m_io.ReadU4le();
            }
            private bool f_dmaTags;
            private DmaTagArrayMap _dmaTags;
            public DmaTagArrayMap DmaTags
            {
                get
                {
                    if (f_dmaTags)
                        return _dmaTags;
                    long _pos = m_io.Pos;
                    m_io.Seek(DmaTagOff);
                    _dmaTags = new DmaTagArrayMap(m_io, this, m_root);
                    m_io.Seek(_pos);
                    f_dmaTags = true;
                    return _dmaTags;
                }
            }
            private uint _dmaTagOff;
            private uint _textureIdx;
            private uint _unk1;
            private uint _unk2;
            private Kh2BarType4ModelData m_root;
            private Kh2BarType4ModelData.MapDesc m_parent;
            public uint DmaTagOff { get { return _dmaTagOff; } }
            public uint TextureIdx { get { return _textureIdx; } }
            public uint Unk1 { get { return _unk1; } }
            public uint Unk2 { get { return _unk2; } }
            public Kh2BarType4ModelData M_Root { get { return m_root; } }
            public Kh2BarType4ModelData.MapDesc M_Parent { get { return m_parent; } }
        }
        public partial class Model : KaitaiStruct
        {
            public static Model FromFile(string fileName)
            {
                return new Model(new KaitaiStream(fileName));
            }

            public Model(KaitaiStream p__io, KaitaiStruct p__parent = null, Kh2BarType4ModelData p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_subModel = false;
                _read();
            }
            private void _read()
            {
                _type = ((Kh2BarType4ModelData.ModelType) m_io.ReadU4le());
                _unk1 = m_io.ReadU4le();
                _unk2 = m_io.ReadU4le();
                _nextOff = m_io.ReadU4le();
                if (Type == Kh2BarType4ModelData.ModelType.Map) {
                    _mapDesc = new MapDesc(m_io, this, m_root);
                }
                if ( ((Type == Kh2BarType4ModelData.ModelType.Object) || (Type == Kh2BarType4ModelData.ModelType.Shadow)) ) {
                    _objectDesc = new ObjectDesc(m_io, this, m_root);
                }
            }
            private bool f_subModel;
            private Model _subModel;
            public Model SubModel
            {
                get
                {
                    if (f_subModel)
                        return _subModel;
                    if (NextOff != 0) {
                        long _pos = m_io.Pos;
                        m_io.Seek(NextOff);
                        __raw_subModel = m_io.ReadBytesFull();
                        var io___raw_subModel = new KaitaiStream(__raw_subModel);
                        _subModel = new Model(io___raw_subModel, this, m_root);
                        m_io.Seek(_pos);
                        f_subModel = true;
                    }
                    return _subModel;
                }
            }
            private ModelType _type;
            private uint _unk1;
            private uint _unk2;
            private uint _nextOff;
            private MapDesc _mapDesc;
            private ObjectDesc _objectDesc;
            private Kh2BarType4ModelData m_root;
            private KaitaiStruct m_parent;
            private byte[] __raw_subModel;
            public ModelType Type { get { return _type; } }
            public uint Unk1 { get { return _unk1; } }
            public uint Unk2 { get { return _unk2; } }
            public uint NextOff { get { return _nextOff; } }
            public MapDesc MapDesc { get { return _mapDesc; } }
            public ObjectDesc ObjectDesc { get { return _objectDesc; } }
            public Kh2BarType4ModelData M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
            public byte[] M_RawSubModel { get { return __raw_subModel; } }
        }
        public partial class DmaChainIndexRemapTable : KaitaiStruct
        {
            public static DmaChainIndexRemapTable FromFile(string fileName)
            {
                return new DmaChainIndexRemapTable(new KaitaiStream(fileName));
            }

            public DmaChainIndexRemapTable(KaitaiStream p__io, Kh2BarType4ModelData.MapDesc p__parent = null, Kh2BarType4ModelData p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _nextOff = m_io.ReadU4le();
                _dmaChainIndex = new List<ushort>();
                {
                    var i = 0;
                    ushort M_;
                    do {
                        M_ = m_io.ReadU2le();
                        _dmaChainIndex.Add(M_);
                        i++;
                    } while (!(M_ == 65535));
                }
            }
            private uint _nextOff;
            private List<ushort> _dmaChainIndex;
            private Kh2BarType4ModelData m_root;
            private Kh2BarType4ModelData.MapDesc m_parent;
            public uint NextOff { get { return _nextOff; } }
            public List<ushort> DmaChainIndex { get { return _dmaChainIndex; } }
            public Kh2BarType4ModelData M_Root { get { return m_root; } }
            public Kh2BarType4ModelData.MapDesc M_Parent { get { return m_parent; } }
        }
        public partial class AxBone : KaitaiStruct
        {
            public static AxBone FromFile(string fileName)
            {
                return new AxBone(new KaitaiStream(fileName));
            }

            public AxBone(KaitaiStream p__io, Kh2BarType4ModelData.ObjectDesc p__parent = null, Kh2BarType4ModelData p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _thisIdx = m_io.ReadU2le();
                _thisReverseIdx = m_io.ReadU2le();
                _parentIdx = m_io.ReadU2le();
                _parentReverseIdx = m_io.ReadU2le();
                _unk1 = m_io.ReadU4le();
                _unk2 = m_io.ReadU4le();
                _scaleX = m_io.ReadF4le();
                _scaleY = m_io.ReadF4le();
                _scaleZ = m_io.ReadF4le();
                _unk3 = m_io.ReadF4le();
                _rotationX = m_io.ReadF4le();
                _rotationY = m_io.ReadF4le();
                _rotationZ = m_io.ReadF4le();
                _unk4 = m_io.ReadU4le();
                _translateX = m_io.ReadF4le();
                _translateY = m_io.ReadF4le();
                _translateZ = m_io.ReadF4le();
                _unk5 = m_io.ReadU4le();
            }
            private ushort _thisIdx;
            private ushort _thisReverseIdx;
            private ushort _parentIdx;
            private ushort _parentReverseIdx;
            private uint _unk1;
            private uint _unk2;
            private float _scaleX;
            private float _scaleY;
            private float _scaleZ;
            private float _unk3;
            private float _rotationX;
            private float _rotationY;
            private float _rotationZ;
            private uint _unk4;
            private float _translateX;
            private float _translateY;
            private float _translateZ;
            private uint _unk5;
            private Kh2BarType4ModelData m_root;
            private Kh2BarType4ModelData.ObjectDesc m_parent;
            public ushort ThisIdx { get { return _thisIdx; } }
            public ushort ThisReverseIdx { get { return _thisReverseIdx; } }
            public ushort ParentIdx { get { return _parentIdx; } }
            public ushort ParentReverseIdx { get { return _parentReverseIdx; } }
            public uint Unk1 { get { return _unk1; } }
            public uint Unk2 { get { return _unk2; } }
            public float ScaleX { get { return _scaleX; } }
            public float ScaleY { get { return _scaleY; } }
            public float ScaleZ { get { return _scaleZ; } }
            public float Unk3 { get { return _unk3; } }
            public float RotationX { get { return _rotationX; } }
            public float RotationY { get { return _rotationY; } }
            public float RotationZ { get { return _rotationZ; } }
            public uint Unk4 { get { return _unk4; } }
            public float TranslateX { get { return _translateX; } }
            public float TranslateY { get { return _translateY; } }
            public float TranslateZ { get { return _translateZ; } }
            public uint Unk5 { get { return _unk5; } }
            public Kh2BarType4ModelData M_Root { get { return m_root; } }
            public Kh2BarType4ModelData.ObjectDesc M_Parent { get { return m_parent; } }
        }
        public partial class SourceChainDmaTag : KaitaiStruct
        {
            public static SourceChainDmaTag FromFile(string fileName)
            {
                return new SourceChainDmaTag(new KaitaiStream(fileName));
            }

            public SourceChainDmaTag(KaitaiStream p__io, KaitaiStruct p__parent = null, Kh2BarType4ModelData p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_endTransfer = false;
                _read();
            }
            private void _read()
            {
                _qwc = m_io.ReadU2le();
                _pad = m_io.ReadBitsIntBe(8);
                _irq = m_io.ReadBitsIntBe(1) != 0;
                _tagId = ((Kh2BarType4ModelData.DmaTagId) m_io.ReadBitsIntBe(3));
                _pce = m_io.ReadBitsIntBe(2);
                m_io.AlignToByte();
                _addr = m_io.ReadU4le();
                _vifTag = new List<VifTag>();
                for (var i = 0; i < 2; i++)
                {
                    _vifTag.Add(new VifTag(m_io, this, m_root));
                }
                if ( (( ((TagId == Kh2BarType4ModelData.DmaTagId.Cnt) || (TagId == Kh2BarType4ModelData.DmaTagId.Next) || (TagId == Kh2BarType4ModelData.DmaTagId.Refe) || (TagId == Kh2BarType4ModelData.DmaTagId.Call) || (TagId == Kh2BarType4ModelData.DmaTagId.Ret)) ) && (VifTag[1].Cmd == Kh2BarType4ModelData.VifCmd.Direct)) ) {
                    _gifTag = new List<GifTag>();
                    for (var i = 0; i < Qwc; i++)
                    {
                        _gifTag.Add(new GifTag(m_io, this, m_root));
                    }
                }
                if ( (( ((TagId == Kh2BarType4ModelData.DmaTagId.Cnt) || (TagId == Kh2BarType4ModelData.DmaTagId.Next) || (TagId == Kh2BarType4ModelData.DmaTagId.Refe) || (TagId == Kh2BarType4ModelData.DmaTagId.Call) || (TagId == Kh2BarType4ModelData.DmaTagId.Ret)) ) && (VifTag[1].Cmd != Kh2BarType4ModelData.VifCmd.Direct)) ) {
                    _rawData = new List<byte[]>();
                    for (var i = 0; i < Qwc; i++)
                    {
                        _rawData.Add(m_io.ReadBytes(16));
                    }
                }
            }
            private bool f_endTransfer;
            private bool _endTransfer;
            public bool EndTransfer
            {
                get
                {
                    if (f_endTransfer)
                        return _endTransfer;
                    _endTransfer = (bool) ( ((TagId == Kh2BarType4ModelData.DmaTagId.Refe) || (TagId == Kh2BarType4ModelData.DmaTagId.End) || (TagId == Kh2BarType4ModelData.DmaTagId.Ret)) );
                    f_endTransfer = true;
                    return _endTransfer;
                }
            }
            private ushort _qwc;
            private ulong _pad;
            private bool _irq;
            private DmaTagId _tagId;
            private ulong _pce;
            private uint _addr;
            private List<VifTag> _vifTag;
            private List<GifTag> _gifTag;
            private List<byte[]> _rawData;
            private Kh2BarType4ModelData m_root;
            private KaitaiStruct m_parent;
            public ushort Qwc { get { return _qwc; } }
            public ulong Pad { get { return _pad; } }
            public bool Irq { get { return _irq; } }
            public DmaTagId TagId { get { return _tagId; } }
            public ulong Pce { get { return _pce; } }
            public uint Addr { get { return _addr; } }
            public List<VifTag> VifTag { get { return _vifTag; } }
            public List<GifTag> GifTag { get { return _gifTag; } }
            public List<byte[]> RawData { get { return _rawData; } }
            public Kh2BarType4ModelData M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        public partial class ObjectDesc : KaitaiStruct
        {
            public static ObjectDesc FromFile(string fileName)
            {
                return new ObjectDesc(new KaitaiStream(fileName));
            }

            public ObjectDesc(KaitaiStream p__io, Kh2BarType4ModelData.Model p__parent = null, Kh2BarType4ModelData p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_axBone = false;
                _read();
            }
            private void _read()
            {
                _numAxbone = m_io.ReadU2le();
                _unk1 = m_io.ReadU2le();
                _offAxbone = m_io.ReadU4le();
                _unk2 = m_io.ReadU4le();
                _cntModelParts = m_io.ReadU2le();
                _unk3 = m_io.ReadU2le();
                _modelParts = new List<ModelPart>();
                for (var i = 0; i < CntModelParts; i++)
                {
                    _modelParts.Add(new ModelPart(m_io, this, m_root));
                }
            }
            private bool f_axBone;
            private List<AxBone> _axBone;
            public List<AxBone> AxBone
            {
                get
                {
                    if (f_axBone)
                        return _axBone;
                    if (OffAxbone != 0) {
                        long _pos = m_io.Pos;
                        m_io.Seek(OffAxbone);
                        _axBone = new List<AxBone>();
                        for (var i = 0; i < NumAxbone; i++)
                        {
                            _axBone.Add(new AxBone(m_io, this, m_root));
                        }
                        m_io.Seek(_pos);
                        f_axBone = true;
                    }
                    return _axBone;
                }
            }
            private ushort _numAxbone;
            private ushort _unk1;
            private uint _offAxbone;
            private uint _unk2;
            private ushort _cntModelParts;
            private ushort _unk3;
            private List<ModelPart> _modelParts;
            private Kh2BarType4ModelData m_root;
            private Kh2BarType4ModelData.Model m_parent;
            public ushort NumAxbone { get { return _numAxbone; } }
            public ushort Unk1 { get { return _unk1; } }
            public uint OffAxbone { get { return _offAxbone; } }
            public uint Unk2 { get { return _unk2; } }
            public ushort CntModelParts { get { return _cntModelParts; } }
            public ushort Unk3 { get { return _unk3; } }
            public List<ModelPart> ModelParts { get { return _modelParts; } }
            public Kh2BarType4ModelData M_Root { get { return m_root; } }
            public Kh2BarType4ModelData.Model M_Parent { get { return m_parent; } }
        }
        public partial class IndicesOfAxbone : KaitaiStruct
        {
            public static IndicesOfAxbone FromFile(string fileName)
            {
                return new IndicesOfAxbone(new KaitaiStream(fileName));
            }

            public IndicesOfAxbone(KaitaiStream p__io, Kh2BarType4ModelData.ModelPart p__parent = null, Kh2BarType4ModelData p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _count = m_io.ReadU4le();
                _indexOfAxbone = new List<uint>();
                for (var i = 0; i < Count; i++)
                {
                    _indexOfAxbone.Add(m_io.ReadU4le());
                }
            }
            private uint _count;
            private List<uint> _indexOfAxbone;
            private Kh2BarType4ModelData m_root;
            private Kh2BarType4ModelData.ModelPart m_parent;
            public uint Count { get { return _count; } }
            public List<uint> IndexOfAxbone { get { return _indexOfAxbone; } }
            public Kh2BarType4ModelData M_Root { get { return m_root; } }
            public Kh2BarType4ModelData.ModelPart M_Parent { get { return m_parent; } }
        }
        public partial class ModelPart : KaitaiStruct
        {
            public static ModelPart FromFile(string fileName)
            {
                return new ModelPart(new KaitaiStream(fileName));
            }

            public ModelPart(KaitaiStream p__io, Kh2BarType4ModelData.ObjectDesc p__parent = null, Kh2BarType4ModelData p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_dmaTags = false;
                f_indicesOfAxbone = false;
                _read();
            }
            private void _read()
            {
                _unk1 = m_io.ReadU4le();
                _textureIndex = m_io.ReadU4le();
                _unk2 = m_io.ReadU4le();
                _unk3 = m_io.ReadU4le();
                _offFirstDmaTag = m_io.ReadU4le();
                _offIndicesOfAxbone = m_io.ReadU4le();
                _numDmaQwcPackets = m_io.ReadU4le();
                _unk5 = m_io.ReadU4le();
            }
            private bool f_dmaTags;
            private DmaTagArrayObject _dmaTags;
            public DmaTagArrayObject DmaTags
            {
                get
                {
                    if (f_dmaTags)
                        return _dmaTags;
                    long _pos = m_io.Pos;
                    m_io.Seek(OffFirstDmaTag);
                    __raw_dmaTags = m_io.ReadBytes((16 * NumDmaQwcPackets));
                    var io___raw_dmaTags = new KaitaiStream(__raw_dmaTags);
                    _dmaTags = new DmaTagArrayObject(io___raw_dmaTags, this, m_root);
                    m_io.Seek(_pos);
                    f_dmaTags = true;
                    return _dmaTags;
                }
            }
            private bool f_indicesOfAxbone;
            private IndicesOfAxbone _indicesOfAxbone;
            public IndicesOfAxbone IndicesOfAxbone
            {
                get
                {
                    if (f_indicesOfAxbone)
                        return _indicesOfAxbone;
                    long _pos = m_io.Pos;
                    m_io.Seek(OffIndicesOfAxbone);
                    _indicesOfAxbone = new IndicesOfAxbone(m_io, this, m_root);
                    m_io.Seek(_pos);
                    f_indicesOfAxbone = true;
                    return _indicesOfAxbone;
                }
            }
            private uint _unk1;
            private uint _textureIndex;
            private uint _unk2;
            private uint _unk3;
            private uint _offFirstDmaTag;
            private uint _offIndicesOfAxbone;
            private uint _numDmaQwcPackets;
            private uint _unk5;
            private Kh2BarType4ModelData m_root;
            private Kh2BarType4ModelData.ObjectDesc m_parent;
            private byte[] __raw_dmaTags;
            public uint Unk1 { get { return _unk1; } }
            public uint TextureIndex { get { return _textureIndex; } }
            public uint Unk2 { get { return _unk2; } }
            public uint Unk3 { get { return _unk3; } }
            public uint OffFirstDmaTag { get { return _offFirstDmaTag; } }
            public uint OffIndicesOfAxbone { get { return _offIndicesOfAxbone; } }
            public uint NumDmaQwcPackets { get { return _numDmaQwcPackets; } }
            public uint Unk5 { get { return _unk5; } }
            public Kh2BarType4ModelData M_Root { get { return m_root; } }
            public Kh2BarType4ModelData.ObjectDesc M_Parent { get { return m_parent; } }
            public byte[] M_RawDmaTags { get { return __raw_dmaTags; } }
        }
        public partial class VifPacketRenderingGroup : KaitaiStruct
        {
            public static VifPacketRenderingGroup FromFile(string fileName)
            {
                return new VifPacketRenderingGroup(new KaitaiStream(fileName));
            }

            public VifPacketRenderingGroup(KaitaiStream p__io, Kh2BarType4ModelData.MapDesc p__parent = null, Kh2BarType4ModelData p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_list = false;
                _read();
            }
            private void _read()
            {
                _offsetToGroup = m_io.ReadU4le();
            }
            private bool f_list;
            private List<ushort> _list;
            public List<ushort> List
            {
                get
                {
                    if (f_list)
                        return _list;
                    KaitaiStream io = M_Parent.M_Io;
                    long _pos = io.Pos;
                    io.Seek(OffsetToGroup);
                    _list = new List<ushort>();
                    {
                        var i = 0;
                        ushort M_;
                        do {
                            M_ = io.ReadU2le();
                            _list.Add(M_);
                            i++;
                        } while (!(M_ == 65535));
                    }
                    io.Seek(_pos);
                    f_list = true;
                    return _list;
                }
            }
            private uint _offsetToGroup;
            private Kh2BarType4ModelData m_root;
            private Kh2BarType4ModelData.MapDesc m_parent;
            public uint OffsetToGroup { get { return _offsetToGroup; } }
            public Kh2BarType4ModelData M_Root { get { return m_root; } }
            public Kh2BarType4ModelData.MapDesc M_Parent { get { return m_parent; } }
        }
        public partial class DmaTagArrayMap : KaitaiStruct
        {
            public static DmaTagArrayMap FromFile(string fileName)
            {
                return new DmaTagArrayMap(new KaitaiStream(fileName));
            }

            public DmaTagArrayMap(KaitaiStream p__io, Kh2BarType4ModelData.DmaChainMap p__parent = null, Kh2BarType4ModelData p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _dmaTag = new List<SourceChainDmaTag>();
                {
                    var i = 0;
                    SourceChainDmaTag M_;
                    do {
                        M_ = new SourceChainDmaTag(m_io, this, m_root);
                        _dmaTag.Add(M_);
                        i++;
                    } while (!(M_.EndTransfer));
                }
            }
            private List<SourceChainDmaTag> _dmaTag;
            private Kh2BarType4ModelData m_root;
            private Kh2BarType4ModelData.DmaChainMap m_parent;
            public List<SourceChainDmaTag> DmaTag { get { return _dmaTag; } }
            public Kh2BarType4ModelData M_Root { get { return m_root; } }
            public Kh2BarType4ModelData.DmaChainMap M_Parent { get { return m_parent; } }
        }
        public partial class MapDesc : KaitaiStruct
        {
            public static MapDesc FromFile(string fileName)
            {
                return new MapDesc(new KaitaiStream(fileName));
            }

            public MapDesc(KaitaiStream p__io, Kh2BarType4ModelData.Model p__parent = null, Kh2BarType4ModelData p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_vifPacketRenderingGroup = false;
                f_dmaChainIndexRemapTable = false;
                _read();
            }
            private void _read()
            {
                _numDmaChainMaps = m_io.ReadU4le();
                _unk3 = m_io.ReadU2le();
                _numVifPacketRenderingGroup = m_io.ReadU2le();
                _offVifPacketRenderingGroup = m_io.ReadU4le();
                _offDmaChainIndexRemapTable = m_io.ReadU4le();
                _dmaChainMap = new List<DmaChainMap>();
                for (var i = 0; i < NumDmaChainMaps; i++)
                {
                    _dmaChainMap.Add(new DmaChainMap(m_io, this, m_root));
                }
            }
            private bool f_vifPacketRenderingGroup;
            private List<VifPacketRenderingGroup> _vifPacketRenderingGroup;
            public List<VifPacketRenderingGroup> VifPacketRenderingGroup
            {
                get
                {
                    if (f_vifPacketRenderingGroup)
                        return _vifPacketRenderingGroup;
                    long _pos = m_io.Pos;
                    m_io.Seek(OffVifPacketRenderingGroup);
                    __raw_vifPacketRenderingGroup = new List<byte[]>();
                    _vifPacketRenderingGroup = new List<VifPacketRenderingGroup>();
                    for (var i = 0; i < NumVifPacketRenderingGroup; i++)
                    {
                        __raw_vifPacketRenderingGroup.Add(m_io.ReadBytes(4));
                        var io___raw_vifPacketRenderingGroup = new KaitaiStream(__raw_vifPacketRenderingGroup[__raw_vifPacketRenderingGroup.Count - 1]);
                        _vifPacketRenderingGroup.Add(new VifPacketRenderingGroup(io___raw_vifPacketRenderingGroup, this, m_root));
                    }
                    m_io.Seek(_pos);
                    f_vifPacketRenderingGroup = true;
                    return _vifPacketRenderingGroup;
                }
            }
            private bool f_dmaChainIndexRemapTable;
            private DmaChainIndexRemapTable _dmaChainIndexRemapTable;
            public DmaChainIndexRemapTable DmaChainIndexRemapTable
            {
                get
                {
                    if (f_dmaChainIndexRemapTable)
                        return _dmaChainIndexRemapTable;
                    long _pos = m_io.Pos;
                    m_io.Seek(OffDmaChainIndexRemapTable);
                    _dmaChainIndexRemapTable = new DmaChainIndexRemapTable(m_io, this, m_root);
                    m_io.Seek(_pos);
                    f_dmaChainIndexRemapTable = true;
                    return _dmaChainIndexRemapTable;
                }
            }
            private uint _numDmaChainMaps;
            private ushort _unk3;
            private ushort _numVifPacketRenderingGroup;
            private uint _offVifPacketRenderingGroup;
            private uint _offDmaChainIndexRemapTable;
            private List<DmaChainMap> _dmaChainMap;
            private Kh2BarType4ModelData m_root;
            private Kh2BarType4ModelData.Model m_parent;
            private List<byte[]> __raw_vifPacketRenderingGroup;
            public uint NumDmaChainMaps { get { return _numDmaChainMaps; } }
            public ushort Unk3 { get { return _unk3; } }
            public ushort NumVifPacketRenderingGroup { get { return _numVifPacketRenderingGroup; } }
            public uint OffVifPacketRenderingGroup { get { return _offVifPacketRenderingGroup; } }
            public uint OffDmaChainIndexRemapTable { get { return _offDmaChainIndexRemapTable; } }
            public List<DmaChainMap> DmaChainMap { get { return _dmaChainMap; } }
            public Kh2BarType4ModelData M_Root { get { return m_root; } }
            public Kh2BarType4ModelData.Model M_Parent { get { return m_parent; } }
            public List<byte[]> M_RawVifPacketRenderingGroup { get { return __raw_vifPacketRenderingGroup; } }
        }
        public partial class VifTag : KaitaiStruct
        {
            public static VifTag FromFile(string fileName)
            {
                return new VifTag(new KaitaiStream(fileName));
            }

            public VifTag(KaitaiStream p__io, Kh2BarType4ModelData.SourceChainDmaTag p__parent = null, Kh2BarType4ModelData p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _immediate = m_io.ReadU2le();
                _num = m_io.ReadU1();
                _cmd = ((Kh2BarType4ModelData.VifCmd) m_io.ReadU1());
            }
            private ushort _immediate;
            private byte _num;
            private VifCmd _cmd;
            private Kh2BarType4ModelData m_root;
            private Kh2BarType4ModelData.SourceChainDmaTag m_parent;
            public ushort Immediate { get { return _immediate; } }
            public byte Num { get { return _num; } }
            public VifCmd Cmd { get { return _cmd; } }
            public Kh2BarType4ModelData M_Root { get { return m_root; } }
            public Kh2BarType4ModelData.SourceChainDmaTag M_Parent { get { return m_parent; } }
        }
        public partial class DmaTagArrayObject : KaitaiStruct
        {
            public static DmaTagArrayObject FromFile(string fileName)
            {
                return new DmaTagArrayObject(new KaitaiStream(fileName));
            }

            public DmaTagArrayObject(KaitaiStream p__io, Kh2BarType4ModelData.ModelPart p__parent = null, Kh2BarType4ModelData p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _dmaTag = new List<SourceChainDmaTag>();
                {
                    var i = 0;
                    while (!m_io.IsEof) {
                        _dmaTag.Add(new SourceChainDmaTag(m_io, this, m_root));
                        i++;
                    }
                }
            }
            private List<SourceChainDmaTag> _dmaTag;
            private Kh2BarType4ModelData m_root;
            private Kh2BarType4ModelData.ModelPart m_parent;
            public List<SourceChainDmaTag> DmaTag { get { return _dmaTag; } }
            public Kh2BarType4ModelData M_Root { get { return m_root; } }
            public Kh2BarType4ModelData.ModelPart M_Parent { get { return m_parent; } }
        }
        private bool f_model;
        private Model _model;
        public Model Model
        {
            get
            {
                if (f_model)
                    return _model;
                long _pos = m_io.Pos;
                m_io.Seek(144);
                __raw_model = m_io.ReadBytesFull();
                var io___raw_model = new KaitaiStream(__raw_model);
                _model = new Model(io___raw_model, this, m_root);
                m_io.Seek(_pos);
                f_model = true;
                return _model;
            }
        }
        private byte[] _hw;
        private Kh2BarType4ModelData m_root;
        private KaitaiStruct m_parent;
        private byte[] __raw_model;
        public byte[] Hw { get { return _hw; } }
        public Kh2BarType4ModelData M_Root { get { return m_root; } }
        public KaitaiStruct M_Parent { get { return m_parent; } }
        public byte[] M_RawModel { get { return __raw_model; } }
    }
}
