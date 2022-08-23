// This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild

(function (root, factory) {
  if (typeof define === 'function' && define.amd) {
    define(['kaitai-struct/KaitaiStream'], factory);
  } else if (typeof module === 'object' && module.exports) {
    module.exports = factory(require('kaitai-struct/KaitaiStream'));
  } else {
    root.Kh2BarType13SpawnPointScript = factory(root.KaitaiStream);
  }
}(this, function (KaitaiStream) {
var Kh2BarType13SpawnPointScript = (function() {
  function Kh2BarType13SpawnPointScript(_io, _parent, _root) {
    this._io = _io;
    this._parent = _parent;
    this._root = _root || this;

    this._read();
  }
  Kh2BarType13SpawnPointScript.prototype._read = function() {
    this.header = []
    var i = 0;
    do {
      var _ = new Header(this._io, this, this._root);
      this.header.push(_);
      i++;
    } while (!(_.id == 65535));
  }

  var ScriptHeader = Kh2BarType13SpawnPointScript.ScriptHeader = (function() {
    function ScriptHeader(_io, _parent, _root) {
      this._io = _io;
      this._parent = _parent;
      this._root = _root || this;

      this._read();
    }
    ScriptHeader.prototype._read = function() {
      this.skip = this._io.readBytes(4);
      this.codeBlock = [];
      var i = 0;
      while (!this._io.isEof()) {
        this.codeBlock.push(new CodeBlock(this._io, this, this._root));
        i++;
      }
    }

    return ScriptHeader;
  })();

  var Spawn = Kh2BarType13SpawnPointScript.Spawn = (function() {
    function Spawn(_io, _parent, _root) {
      this._io = _io;
      this._parent = _parent;
      this._root = _root || this;

      this._read();
    }
    Spawn.prototype._read = function() {
      this.data = KaitaiStream.bytesToStr(this._io.readBytes(4), "ascii");
    }

    return Spawn;
  })();

  var Unk = Kh2BarType13SpawnPointScript.Unk = (function() {
    function Unk(_io, _parent, _root) {
      this._io = _io;
      this._parent = _parent;
      this._root = _root || this;

      this._read();
    }
    Unk.prototype._read = function() {
      this.dummy = [];
      var i = 0;
      while (!this._io.isEof()) {
        this.dummy.push(this._io.readBytes(1));
        i++;
      }
    }

    return Unk;
  })();

  var Nop = Kh2BarType13SpawnPointScript.Nop = (function() {
    function Nop(_io, _parent, _root) {
      this._io = _io;
      this._parent = _parent;
      this._root = _root || this;

      this._read();
    }
    Nop.prototype._read = function() {
      this.data = this._io.readBytes(0);
    }

    return Nop;
  })();

  var Type0c = Kh2BarType13SpawnPointScript.Type0c = (function() {
    function Type0c(_io, _parent, _root) {
      this._io = _io;
      this._parent = _parent;
      this._root = _root || this;

      this._read();
    }
    Type0c.prototype._read = function() {
      this.data = this._io.readBytes(34);
    }

    return Type0c;
  })();

  var MapOcclusion = Kh2BarType13SpawnPointScript.MapOcclusion = (function() {
    function MapOcclusion(_io, _parent, _root) {
      this._io = _io;
      this._parent = _parent;
      this._root = _root || this;

      this._read();
    }
    MapOcclusion.prototype._read = function() {
      this.data = this._io.readBytes(6);
    }

    return MapOcclusion;
  })();

  var CodeBlock = Kh2BarType13SpawnPointScript.CodeBlock = (function() {
    function CodeBlock(_io, _parent, _root) {
      this._io = _io;
      this._parent = _parent;
      this._root = _root || this;

      this._read();
    }
    CodeBlock.prototype._read = function() {
      this.type = this._io.readU2le();
      switch (this.type) {
      case 0:
        this.argument = new Nop(this._io, this, this._root);
        break;
      case 7:
        this.argument = new MapOcclusion(this._io, this, this._root);
        break;
      case 1:
        this.argument = new Spawn(this._io, this, this._root);
        break;
      case 12:
        this.argument = new Type0c(this._io, this, this._root);
        break;
      default:
        this.argument = new Unk(this._io, this, this._root);
        break;
      }
    }

    return CodeBlock;
  })();

  var Header = Kh2BarType13SpawnPointScript.Header = (function() {
    function Header(_io, _parent, _root) {
      this._io = _io;
      this._parent = _parent;
      this._root = _root || this;

      this._read();
    }
    Header.prototype._read = function() {
      this.id = this._io.readU2le();
      this.len = this._io.readU2le();
      if (this.len != 0) {
        this.script = this._io.readBytes((this.len - 4));
      }
    }

    return Header;
  })();

  return Kh2BarType13SpawnPointScript;
})();
return Kh2BarType13SpawnPointScript;
}));
