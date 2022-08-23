meta:
  id: kh2_bar_type_13_spawn_point_script
  file-extension: bin
  endian: le
seq:
  - id: header
    type: header
    repeat: until
    repeat-until: _.id == 0xffff
types:
  header:
    seq:
      - id: id
        type: u2
      - id: len
        type: u2
      - id: script
        if: len != 0
        size: len - 4
        type: script_header
  script_header:
    seq:
      - id: skip
        size: 4
      - id: code_block
        type: code_block
        repeat: eos
        #repeat: until
        #repeat-until: _.type == 1
  code_block:
    seq:
      - id: type
        type: u2
      - id: argument
        type:
          switch-on: type
          cases:
            12: type_0c
            7: map_occlusion
            1: spawn
            0: nop
            _: unk
  unk:
    seq:
      - id: dummy
        size: 1
        repeat: eos
  type_0c:
    seq:
      - id: data
        size: 34
  nop:
    seq:
      - id: data
        size: 0
  spawn:
    seq:
      - id: data
        size: 4
        type: str
        encoding: ascii
  map_occlusion:
    seq:
      - id: data
        size: 6
