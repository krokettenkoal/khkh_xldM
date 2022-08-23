meta:
  id: kh2_bar
  endian: le
seq:
  - id: magic
    contents: [0x42, 0x41, 0x52, 0x01]
  - id: num_files
    type: s4
  - id: padding
    size: 8
  - id: files
    type: file_entry
    repeat: expr
    repeat-expr: num_files
types:
  file_entry:
    seq:
      - id: type
        type: u2
      - id: duplicate
        type: u2
      - id: name
        type: str
        size: 4
        encoding: UTF-8
      - id: offset
        type: s4
      - id: size
        type: s4
    instances:
      file:
        io: _root._io
        pos: offset
        size: size
        type:
          switch-on: name
          cases:
            '"ftst"': ftst
            '"item"': item
  table_header:
    seq:
      - id: magic_code
        type: u4
      - id: count
        type: u4
  ftst:
    seq:
      - id: header
        type: table_header
      - id: entries
        type: entry
        repeat: expr
        repeat-expr: header.count
    types:
      entry:
        seq:
          - id: id
            type: u4
          - id: colors
            type: u4
            repeat: expr
            repeat-expr: 19
  item:
    seq:
      - id: entry_header
        type: table_header
      - id: entries
        type: entry
        repeat: expr
        repeat-expr: entry_header.count
      - id: stat_header
        type: table_header
      - id: stats
        type: stat
        repeat: expr
        repeat-expr: stat_header.count
    enums:
      item_type:
        0: 'consumable'
        1: 'boost'
        2: 'keyblade'
        3: 'staff'
        4: 'shield'
        5: 'pingweapon'
        6: 'auronweapon'
        7: 'beastweapon'
        8: 'jackweapon'
        9: 'dummyweapon'
        10: 'rikuweapon'
        11: 'simbaweapon'
        12: 'jacksparrowweapon'
        13: 'tronweapon'
        14: 'armor'
        15: 'accessory'
        16: 'synthesis'
        17: 'recipe'
        18: 'magic'
        19: 'ability'
        20: 'summon'
        21: 'form'
        22: 'map'
        23: 'report'
      item_rank:
        0: c
        1: b
        2: a
        3: s
    types:
      entry:
        seq:
          - id: id
            type: u2
          - id: type
            type: u1
            enum: item_type
          - id: flag0
            type: u1
          - id: flag1
            type: u1
          - id: rank
            type: u1
            enum: item_rank
          - id: stat_entry
            type: u2
          - id: name
            type: u2
          - id: description
            type: u2
          - id: shop_buy
            type: u2
          - id: shop_sell
            type: u2
          - id: command
            type: u2
          - id: slot
            type: u2
          - id: picture
            type: u2
          - id: icon1
            type: u1
          - id: icon2
            type: u1
      stat:
        seq:
          - id: id
            type: u2
          - id: ability
            type: u2
          - id: attack
            type: u1
          - id: magic
            type: u1
          - id: defense
            type: u1
          - id: ability_points
            type: u1
          - id: unknown08
            type: u1
          - id: fire_resistance
            type: u1
          - id: ice_resistance
            type: u1
          - id: lightning_resistance
            type: u1
          - id: dark_resistance
            type: u1
          - id: unknown0d
            type: u1
          - id: general_resistance
            type: u1
          - id: unknown
            type: u1
            