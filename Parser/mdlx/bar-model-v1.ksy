meta:
  id: bar_model_ksy
  file-extension: mdlx
  endian: le
seq:
  - id: sig
    size: 4
    type: str
    encoding: latin1
  - id: sub_file_count
    type: u4
  - id: unk8
    type: u4
  - id: mset_type
    type: u4
  - id: entries
    type: bar_entry
    repeat: expr
    repeat-expr: sub_file_count
types:
  bar_entry:
    seq:
      - id: type
        type: u2
      - id: duplicate
        type: u2
      - id: name
        size: 4
        type: str
        encoding: latin1
      - id: offset
        type: u4
      - id: size
        type: u4
    instances:
      model:
        pos: offset
        size: size
        type: model_header
        if: type == 4
  model_header:
    seq:
      - id: hardware
        size: 0x90
      - id: version
        type: u2
        enum: model_type
      - id: unk4
        size: 10
      - id: next_model_header
        type: u4
      - id: bone_count
        type: u2
      - id: unk12
        type: u2
      - id: bone_offset
        type: u4
      - id: unk18
        type: u4
      - id: model_subpart_count
        type: u2
      - id: unk1e
        type: u2
    instances:
      next_model:
        pos: next_model_header
        if: next_model_header != 0
        type: model_header
enums:
  model_type:
    2: "map"
    3: "object"
    4: "shadow"
