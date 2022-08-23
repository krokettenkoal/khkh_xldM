meta:
  id: pax
  file-extension: bin
  application: Kingdom Hearts II PAX binary
  endian: le
seq:
  - id: magic
    contents: 'PAX_'
  - id: ofs_name
    type: u4
  - id: num_entries
    type: u4
  - id: ofs_dpx
    type: u4
instances:
  entries:
    type: entry
    repeat: expr
    repeat-expr: num_entries
  dpx:
    type: dpx
    pos: ofs_dpx
    size-eos: true
types:
  entry:
    seq:
      - id: effect
        type: u2
      - id: caster
        type: u2
      - id: unk04
        type: u2
      - id: unk06
        type: u2
      - id: unk08
        type: u4
      - id: unk0c
        type: u4
      - id: unk10
        type: u4
      - id: unk14
        type: u4
      - id: sound_effect
        type: s4
      - id: pos_x
        type: f4
      - id: pos_z
        type: f4
      - id: pos_y
        type: f4
      - id: rot_x
        type: f4
      - id: rot_z
        type: f4
      - id: rot_y
        type: f4
      - id: scale_x
        type: f4
      - id: scale_z
        type: f4
      - id: scale_y
        type: f4
      - id: unk40
        type: u4
      - id: unk44
        type: u4
      - id: unk48
        type: u4
      - id: unk4c
        type: u4
  dpx:
    seq:
      - id: magic
        contents: [0x82,0,0,0]
      - id: unk04
        type: u4
      - id: unk08
        type: u4
      - id: num_dpd_ref
        type: u4
    instances:
      dpd_ref:
        size: 32
        type: dpd_ref
        repeat: expr
        repeat-expr: num_dpd_ref
  dpd_ref:
    seq:
      - id: ofs_dpd
        type: u4
      - id: unk04
        type: u4
      - id: unk08
        type: u4
