// This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild

(function (root, factory) {
  if (typeof define === 'function' && define.amd) {
    define(['kaitai-struct/KaitaiStream'], factory);
  } else if (typeof module === 'object' && module.exports) {
    module.exports = factory(require('kaitai-struct/KaitaiStream'));
  } else {
    root.Kh2ModelTex = factory(root.KaitaiStream);
  }
}(this, function (KaitaiStream) {
var Kh2ModelTex = (function() {
  function Kh2ModelTex(_io, _parent, _root) {
    this._io = _io;
    this._parent = _parent;
    this._root = _root || this;

    this._read();
  }
  Kh2ModelTex.prototype._read = function() {
    this.magicCode = this._io.readU4le();
    this.colorCount = this._io.readU4le();
    this.textureInfoCount = this._io.readU4le();
    this.gsInfoCount = this._io.readU4le();
    this.offset1 = this._io.readU4le();
    this.texinf1off = this._io.readU4le();
    this.texinf2off = this._io.readU4le();
    this.pictureOffset = this._io.readU4le();
    this.paletteOffset = this._io.readU4le();
  }

  var GsInfo = Kh2ModelTex.GsInfo = (function() {
    function GsInfo(_io, _parent, _root) {
      this._io = _io;
      this._parent = _parent;
      this._root = _root || this;

      this._read();
    }
    GsInfo.prototype._read = function() {
      this.data00 = this._io.readF8le();
      this.data08 = this._io.readF8le();
      this.data10 = this._io.readF8le();
      this.data18 = this._io.readF8le();
      this.data20 = this._io.readF8le();
      this.data28 = this._io.readF8le();
      this.data30 = this._io.readF8le();
      this.data38 = this._io.readF8le();
      this.data40 = this._io.readF8le();
      this.data48 = this._io.readF8le();
      this.data50 = this._io.readF8le();
      this.data58 = this._io.readF8le();
      this.data60 = this._io.readF8le();
      this.data68 = this._io.readF8le();
      this.gsTex0 = this._io.readU8le();
      this.data80 = this._io.readF8le();
      this.addressMode = this._io.readU8le();
      this.data88 = this._io.readF8le();
      this.data90 = this._io.readF8le();
      this.data98 = this._io.readF8le();
    }

    return GsInfo;
  })();

  var TextureInfo = Kh2ModelTex.TextureInfo = (function() {
    function TextureInfo(_io, _parent, _root) {
      this._io = _io;
      this._parent = _parent;
      this._root = _root || this;

      this._read();
    }
    TextureInfo.prototype._read = function() {
      this.data00 = this._io.readU4le();
      this.data04 = this._io.readU4le();
      this.data08 = this._io.readU4le();
      this.data0c = this._io.readU4le();
      this.data10 = this._io.readU4le();
      this.data14 = this._io.readU4le();
      this.data18 = this._io.readU4le();
      this.data1c = this._io.readU4le();
      this.data20 = this._io.readU4le();
      this.data24 = this._io.readU4le();
      this.data28 = this._io.readU4le();
      this.data2c = this._io.readU4le();
      this.data30 = this._io.readU4le();
      this.data34 = this._io.readU4le();
      this.data38 = this._io.readU4le();
      this.data3c = this._io.readU4le();
      this.data40 = this._io.readU4le();
      this.data44 = this._io.readU4le();
      this.data48 = this._io.readU4le();
      this.data4c = this._io.readU4le();
      this.data50 = this._io.readU4le();
      this.data54 = this._io.readU4le();
      this.data58 = this._io.readU4le();
      this.data5c = this._io.readU4le();
      this.data60 = this._io.readU4le();
      this.data64 = this._io.readU4le();
      this.data68 = this._io.readU4le();
      this.data6c = this._io.readU4le();
      this.data70 = this._io.readU4le();
      this.pictureOffset = this._io.readU4le();
      this.data78 = this._io.readU4le();
      this.data7c = this._io.readU4le();
      this.data80 = this._io.readU4le();
      this.data84 = this._io.readU4le();
      this.data88 = this._io.readU4le();
      this.data8c = this._io.readU4le();
    }

    return TextureInfo;
  })();
  Object.defineProperty(Kh2ModelTex.prototype, 'pictureData', {
    get: function() {
      if (this._m_pictureData !== undefined)
        return this._m_pictureData;
      var _pos = this._io.pos;
      this._io.seek(this.pictureOffset);
      this._m_pictureData = this._io.readBytes((this.paletteOffset - this.pictureOffset));
      this._io.seek(_pos);
      return this._m_pictureData;
    }
  });
  Object.defineProperty(Kh2ModelTex.prototype, 'paletteData', {
    get: function() {
      if (this._m_paletteData !== undefined)
        return this._m_paletteData;
      var _pos = this._io.pos;
      this._io.seek(this.paletteOffset);
      this._m_paletteData = this._io.readBytes((4 * this.colorCount));
      this._io.seek(_pos);
      return this._m_paletteData;
    }
  });
  Object.defineProperty(Kh2ModelTex.prototype, 'texinf2', {
    get: function() {
      if (this._m_texinf2 !== undefined)
        return this._m_texinf2;
      var _pos = this._io.pos;
      this._io.seek(this.texinf2off);
      this._m_texinf2 = new Array(this.gsInfoCount);
      for (var i = 0; i < this.gsInfoCount; i++) {
        this._m_texinf2[i] = new GsInfo(this._io, this, this._root);
      }
      this._io.seek(_pos);
      return this._m_texinf2;
    }
  });
  Object.defineProperty(Kh2ModelTex.prototype, 'texinf1', {
    get: function() {
      if (this._m_texinf1 !== undefined)
        return this._m_texinf1;
      var _pos = this._io.pos;
      this._io.seek(this.texinf1off);
      this._m_texinf1 = new Array(this.textureInfoCount);
      for (var i = 0; i < this.textureInfoCount; i++) {
        this._m_texinf1[i] = new TextureInfo(this._io, this, this._root);
      }
      this._io.seek(_pos);
      return this._m_texinf1;
    }
  });
  Object.defineProperty(Kh2ModelTex.prototype, 'offsetData', {
    get: function() {
      if (this._m_offsetData !== undefined)
        return this._m_offsetData;
      var _pos = this._io.pos;
      this._io.seek(this.offset1);
      this._m_offsetData = this._io.readBytes(this.gsInfoCount);
      this._io.seek(_pos);
      return this._m_offsetData;
    }
  });

  return Kh2ModelTex;
})();
return Kh2ModelTex;
}));
