meta:
  id: kh2_model_tex
  file-extension: bin
  xref: https://openkh.dev/kh2/file/raw-texture.html
  endian: le
seq:
  - id: magic_code
    type: u4
  - id: color_count
    type: u4
  - id: texture_info_count
    type: u4
  - id: gs_info_count
    type: u4
  - id: offset1
    type: u4
  - id: texinf1off
    type: u4
  - id: texinf2off
    type: u4
  - id: picture_offset
    type: u4
  - id: palette_offset
    type: u4
instances:
  offset_data:
    pos: offset1
    size: gs_info_count
  clut_inf:
    pos: texinf1off
    type: texture_info
  texinf1:
    pos: texinf1off + 0x90
    type: texture_info
    repeat: expr
    repeat-expr: texture_info_count
  texinf2:
    pos: texinf2off
    type: gs_info
    repeat: expr
    repeat-expr: gs_info_count
  picture_data:
    pos: picture_offset
    size: palette_offset - picture_offset
  palette_data:
    pos: palette_offset
    size: 4 * color_count
    
types:
  gs_info:
    seq:
      - id: data00
        type: u8
      - id: data08
        type: u8
      - id: data10
        type: u8
      - id: data18
        type: u8
      - id: data20
        type: u8
      - id: data28
        type: u8
      - id: data30
        type: u8
      - id: data38
        type: u8
      - id: data40
        type: u8
      - id: data48
        type: u8
      - id: data50
        type: u8
      - id: data58
        type: u8
      - id: data60
        type: u8
      - id: data68
        type: u8
      - id: gs_tex0
        type: u8
      - id: data80
        type: u8
      - id: address_mode
        type: u8
      - id: data88
        type: u8
      - id: data90
        type: u8
      - id: data98
        type: u8

  texture_info:
    seq:
      - id: data00
        type: u4
      - id: data04
        type: u4
      - id: data08
        type: u4
      - id: data0c
        type: u4
      - id: data10
        type: u4
      - id: data14
        type: u4
      - id: data18
        type: u4
      - id: data1c
        type: u4
      - id: data20
        type: u8
      - id: data28
        type: u8
      - id: data30
        type: u8
      - id: data38
        type: u8
      - id: data40
        type: u8
      - id: data48
        type: u8
      - id: data50
        type: u8
      - id: data58
        type: u8
      - id: data60
        type: u8
      - id: data68
        type: u8
      - id: data70
        type: u4
      - id: picture_offset
        type: u4
      - id: data78
        type: u4
      - id: data7c
        type: u4
      - id: data80
        type: u4
      - id: data84
        type: u4
      - id: data88
        type: u4
      - id: data8c
        type: u4
