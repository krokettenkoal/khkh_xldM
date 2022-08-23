// This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild

(function (root, factory) {
  if (typeof define === 'function' && define.amd) {
    define(['kaitai-struct/KaitaiStream'], factory);
  } else if (typeof module === 'object' && module.exports) {
    module.exports = factory(require('kaitai-struct/KaitaiStream'));
  } else {
    root.Kh2Bar = factory(root.KaitaiStream);
  }
}(this, function (KaitaiStream) {
var Kh2Bar = (function() {
  function Kh2Bar(_io, _parent, _root) {
    this._io = _io;
    this._parent = _parent;
    this._root = _root || this;

    this._read();
  }
  Kh2Bar.prototype._read = function() {
    this.magic = this._io.ensureFixedContents([66, 65, 82, 1]);
    this.numFiles = this._io.readS4le();
    this.padding = this._io.readBytes(8);
    this.files = new Array(this.numFiles);
    for (var i = 0; i < this.numFiles; i++) {
      this.files[i] = new FileEntry(this._io, this, this._root);
    }
  }

  var FileEntry = Kh2Bar.FileEntry = (function() {
    function FileEntry(_io, _parent, _root) {
      this._io = _io;
      this._parent = _parent;
      this._root = _root || this;

      this._read();
    }
    FileEntry.prototype._read = function() {
      this.type = this._io.readU2le();
      this.duplicate = this._io.readU2le();
      this.name = KaitaiStream.bytesToStr(this._io.readBytes(4), "UTF-8");
      this.offset = this._io.readS4le();
      this.size = this._io.readS4le();
    }
    Object.defineProperty(FileEntry.prototype, 'file', {
      get: function() {
        if (this._m_file !== undefined)
          return this._m_file;
        var io = this._root._io;
        var _pos = io.pos;
        io.seek(this.offset);
        switch (this.name) {
        case "ftst":
          this._raw__m_file = io.readBytes(this.size);
          var _io__raw__m_file = new KaitaiStream(this._raw__m_file);
          this._m_file = new Ftst(_io__raw__m_file, this, this._root);
          break;
        case "item":
          this._raw__m_file = io.readBytes(this.size);
          var _io__raw__m_file = new KaitaiStream(this._raw__m_file);
          this._m_file = new Item(_io__raw__m_file, this, this._root);
          break;
        default:
          this._m_file = io.readBytes(this.size);
          break;
        }
        io.seek(_pos);
        return this._m_file;
      }
    });

    return FileEntry;
  })();

  var TableHeader = Kh2Bar.TableHeader = (function() {
    function TableHeader(_io, _parent, _root) {
      this._io = _io;
      this._parent = _parent;
      this._root = _root || this;

      this._read();
    }
    TableHeader.prototype._read = function() {
      this.magicCode = this._io.readU4le();
      this.count = this._io.readU4le();
    }

    return TableHeader;
  })();

  var Ftst = Kh2Bar.Ftst = (function() {
    function Ftst(_io, _parent, _root) {
      this._io = _io;
      this._parent = _parent;
      this._root = _root || this;

      this._read();
    }
    Ftst.prototype._read = function() {
      this.header = new TableHeader(this._io, this, this._root);
      this.entries = new Array(this.header.count);
      for (var i = 0; i < this.header.count; i++) {
        this.entries[i] = new Entry(this._io, this, this._root);
      }
    }

    var Entry = Ftst.Entry = (function() {
      function Entry(_io, _parent, _root) {
        this._io = _io;
        this._parent = _parent;
        this._root = _root || this;

        this._read();
      }
      Entry.prototype._read = function() {
        this.id = this._io.readU4le();
        this.colors = new Array(19);
        for (var i = 0; i < 19; i++) {
          this.colors[i] = this._io.readU4le();
        }
      }

      return Entry;
    })();

    return Ftst;
  })();

  var Item = Kh2Bar.Item = (function() {
    Item.ItemType = Object.freeze({
      CONSUMABLE: 0,
      BOOST: 1,
      KEYBLADE: 2,
      STAFF: 3,
      SHIELD: 4,
      PINGWEAPON: 5,
      AURONWEAPON: 6,
      BEASTWEAPON: 7,
      JACKWEAPON: 8,
      DUMMYWEAPON: 9,
      RIKUWEAPON: 10,
      SIMBAWEAPON: 11,
      JACKSPARROWWEAPON: 12,
      TRONWEAPON: 13,
      ARMOR: 14,
      ACCESSORY: 15,
      SYNTHESIS: 16,
      RECIPE: 17,
      MAGIC: 18,
      ABILITY: 19,
      SUMMON: 20,
      FORM: 21,
      MAP: 22,
      REPORT: 23,

      0: "CONSUMABLE",
      1: "BOOST",
      2: "KEYBLADE",
      3: "STAFF",
      4: "SHIELD",
      5: "PINGWEAPON",
      6: "AURONWEAPON",
      7: "BEASTWEAPON",
      8: "JACKWEAPON",
      9: "DUMMYWEAPON",
      10: "RIKUWEAPON",
      11: "SIMBAWEAPON",
      12: "JACKSPARROWWEAPON",
      13: "TRONWEAPON",
      14: "ARMOR",
      15: "ACCESSORY",
      16: "SYNTHESIS",
      17: "RECIPE",
      18: "MAGIC",
      19: "ABILITY",
      20: "SUMMON",
      21: "FORM",
      22: "MAP",
      23: "REPORT",
    });

    Item.ItemRank = Object.freeze({
      C: 0,
      B: 1,
      A: 2,
      S: 3,

      0: "C",
      1: "B",
      2: "A",
      3: "S",
    });

    function Item(_io, _parent, _root) {
      this._io = _io;
      this._parent = _parent;
      this._root = _root || this;

      this._read();
    }
    Item.prototype._read = function() {
      this.header = new TableHeader(this._io, this, this._root);
      this.entries = new Array(this.header.count);
      for (var i = 0; i < this.header.count; i++) {
        this.entries[i] = new Entry(this._io, this, this._root);
      }
    }

    var Entry = Item.Entry = (function() {
      function Entry(_io, _parent, _root) {
        this._io = _io;
        this._parent = _parent;
        this._root = _root || this;

        this._read();
      }
      Entry.prototype._read = function() {
        this.id = this._io.readU2le();
        this.type = this._io.readU1();
        this.flag0 = this._io.readU1();
        this.flag1 = this._io.readU1();
        this.rank = this._io.readU1();
        this.statEntry = this._io.readU2le();
        this.name = this._io.readU2le();
        this.description = this._io.readU2le();
        this.shopBuy = this._io.readU2le();
        this.shopSell = this._io.readU2le();
        this.command = this._io.readU2le();
        this.slot = this._io.readU2le();
        this.picture = this._io.readU2le();
        this.icon1 = this._io.readU1();
        this.icon2 = this._io.readU1();
      }

      return Entry;
    })();

    return Item;
  })();

  return Kh2Bar;
})();
return Kh2Bar;
}));
