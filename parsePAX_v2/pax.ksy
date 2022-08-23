meta:
  id: pax
  file-extension: bin
  application: Kingdom Hearts II PAX binary
  endian: le
seq:
  - id: magic
    contents: 'PAX_'
  - id: ofs_header1
    type: u4
  - id: len_dat1
    type: u4
  - id: ofs_header2
    type: u4
  - id: dat1_list
    type: dat1
    repeat: expr
    repeat-expr: len_dat1
instances:
  header1:
    pos: ofs_header1
    type: header1
  header2:
    pos: ofs_header2
    type: header2
types:
  header1:
    seq:
      - id: magic
        type: u4
  dat1:
    seq:
      - id: v00
        type: u2
      - id: v02
        type: u2
      - id: v04
        type: u2
      - id: v06
        type: u2
        
      - id: v08
        type: u4
      - id: v0c
        type: u4
      - id: v10
        type: u4
      - id: v14
        type: u4
      - id: v18
        type: u4

      - id: v1c
        type: f4
      - id: v20
        type: f4
      - id: v24
        type: f4
      - id: v28
        type: f4
      - id: v2c
        type: f4
      - id: v30
        type: f4
      - id: v34
        type: f4
      - id: v38
        type: f4
      - id: v3c
        type: f4

      - id: v40
        type: u4
      - id: v44
        type: u4
      - id: v48
        type: u4
      - id: v4c
        type: u4
  header2:
    seq:
      - id: magic
        contents: [0x82, 0, 0, 0]
      - id: v04
        type: u4
      - id: v08
        type: u4
      - id: len_dat2
        type: u4
      - id: dat2
        type: dat2
        size: 32
        repeat: expr
        repeat-expr: len_dat2
  dat2:
    seq:
      - id: ofs_header3
        type: u4
      - id: v04
        type: u4
      - id: v08
        type: u4
      - id: v0c
        type: u4
      - id: v10
        type: u4
      - id: v14
        type: u4
      - id: v18
        type: u4
      - id: v1c
        type: u4
    instances:
      header3:
        pos: _root.ofs_header2 + ofs_header3
        type: header3
        size-eos: true
  header3:
    seq:
      - id: magic
        contents: [0x96, 0, 0, 0]

      - id: len_dat31
        type: u4
      - id: ofs_dat31
        type: u4
        repeat: expr
        repeat-expr: len_dat31

      - id: len_dat32
        type: u4
      - id: ofs_dat32
        type: u4
        repeat: expr
        repeat-expr: len_dat32

      - id: len_dat33
        type: u4
      - id: ofs_dat33
        type: u4
        repeat: expr
        repeat-expr: len_dat33

      - id: len_dat34
        type: u4
      - id: ofs_dat34
        type: u4
        repeat: expr
        repeat-expr: len_dat34

      - id: len_dat35
        type: u4
      - id: ofs_dat35
        type: u4
        repeat: expr
        repeat-expr: len_dat35
    instances:
      dat31:
        pos: ofs_dat31[_index]
        type: dat31
        repeat: expr
        repeat-expr: len_dat31
  dat31:
    seq:
      - id: vertices
        type: dat31_vertex
        repeat: expr
        repeat-expr: 8
      - id: v80
        type: vector4
      - id: v90
        type: vector4
      - id: va0
        type: vector4
      - id: vb0
        type: vector4
      - id: vc0
        size: 80
  dat31_vertex:
    seq:
      - id: x0
        type: s2
      - id: y0
        type: s2
      - id: x1
        type: s2
      - id: y1
        type: s2
      - id: x2
        type: s2
      - id: y2
        type: s2
      - id: w
        type: f4
  vector4:
    seq:
      - id: x
        type: f4
      - id: y
        type: f4
      - id: z
        type: f4
      - id: w
        type: f4
        