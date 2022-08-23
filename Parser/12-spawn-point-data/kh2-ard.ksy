meta:
  id: kh2_bar_type_12_spawn_point_data
  file-extension: bin
  endian: le
seq:
  - id: header
    type: header
  - id: spawn_point
    type: spawn_point
  - id: spawn_entiy_group 
    type: entity
    repeat: expr
    repeat-expr: spawn_point.spawn_entity_group_count
  - id: entity_group2
    type: entity
    repeat: expr
    repeat-expr: spawn_point.entity_group2_count
types:
  header:
    seq:
      - id: type_id
        type: u4
      - id: item_count
        type: u4
  spawn_point:
    seq:
      - id: unk00
        type: u2
      - id: unk02
        type: u2
      - id: spawn_entity_group_count
        type: u2
      - id: entity_group2_count
        type: u2
      - id: unk08
        type: u4
      - id: unk0c
        type: u4
      - id: unk10
        type: u4
      - id: unk14
        type: u4
      - id: unk18
        type: u4
      - id: unk1c
        type: u4
      - id: unk20
        type: u4
      - id: unk24
        type: u4
      - id: unk28
        type: u4
  entity:
    doc-ref: OpenKh\OpenKh.Kh2\Ard\SpawnPoint.cs
    seq:
      - id: object_id
        type: u2
        enum: obj_name
      - id: pad
        type: u2
      - id: position_x
        type: f4
      - id: position_y
        type: f4
      - id: position_z
        type: f4
      - id: rotation_x
        type: f4
      - id: rotation_y
        type: f4
      - id: rotation_z
        type: f4
      - id: unk_1c
        type: u2
      - id: unk_1e
        type: u2
      - id: unk_20
        type: u4
      - id: unk_24
        type: u4
      - id: unk_28
        type: u4
      - id: unk_2c
        type: u4
      - id: unk_30
        type: u4
      - id: unk_34
        type: u4
      - id: unk_38
        type: u4
      - id: unk_3c
        type: u4
enums:
  obj_name:
    0x0001: {id: m_ex060, doc: 'Böllerwampe / Fat Bandit (Gegner! / Enemy!)'}
    0x0002: {id: m_ex500, doc: 'Schizo-Spuk / Trick Ghost (Gegner! / Enemy!)'}
    0x0003: {id: m_ex510, doc: 'Schoßhund / Rabid Dog (Gegner! / Enemy!)'}
    0x0004: {id: m_ex520, doc: 'Flederhaken / Hook Bat (Gegner! / Enemy!)'}
    0x0005: {id: m_ex530, doc: 'Magus Magnus / Bookmaster (Gegner! / Enemy!)'}
    0x0006: {id: m_ex540, doc: 'Blauer Baron / Aeroplane (Gegner! / Enemy!)'}
    0x0007: {id: m_ex550, doc: 'Bombenmolch / Minute Bomb (Gegner! / Enemy!)'}
    0x0008: {id: m_ex560, doc: 'Hammerlakai / Hammer Frame (Gegner! / Enemy!)'}
    0x0009: {id: m_ex570, doc: 'Zentaurier / Assault Rider (Gegner! / Enemy!)'}
    0x000A: {id: m_ex580, doc: 'Schreck-Geck / Nightwalker (Gegner! / Enemy!)'}
    0x000B: {id: m_ex620, doc: 'Wahrsagerin / Fortuneteller (Gegner! / Enemy!)'}
    0x000C: {id: m_ex630, doc: 'Mondräuber / Luna Knight (Gegner! / Enemy!)'}
    0x000D: {id: m_ex640, doc: 'Retro-Raser / Hot Rod (Gegner! / Enemy!)'}
    0x000E: {id: m_ex650, doc: 'Böllerbolzen / Cannon Gun (Gegner! / Enemy!)'}
    0x000F: {id: m_ex670, doc: 'Schädelbulle / Living Bone (ohne Schamane / without Shaman) (Gegner! / Enemy!)'}
    0x0010: {id: m_ex680, doc: 'Transbot / Devastator (Gegner! / Enemy!)'}
    0x0011: {id: m_ex690, doc: 'Lanzensoldat / Lance Soldier (Gegner! / Enemy!)'}
    0x0012: {id: m_ex700, doc: 'Drilling / Mole Driller (Gegner! / Enemy!)'}
    0x0013: {id: m_ex720, doc: 'Shamane / Shaman (Gegner! / Enemy!)'}
    0x0014: {id: m_ex780, doc: 'Flugboxer / Aerial Knocker (Gegner! / Enemy!)'}
    0x0015: {id: b_mu100, doc: 'Shan-Yu (Boss!)'}
    0x0016: {id: b_mu110, doc: 'Hayabusa (Shan-Yus Falke / Shan-Yu''s Falcon) (Boss!)'}
    0x0017: {id: f_he000, doc: 'Fackelständer/ Standing Torch (Objekt / Object)'}
    0x0018: {id: f_he010, doc: 'Blaue Barriere / Blue Barrier (Objekt / Object)'}
    0x0019: {id: f_he110, doc: 'Nebelkugel / Mist Sphere (Objekt / Object)'}
    0x001A: {id: f_bb040, doc: 'Unsichtare Rüstung / "Invisible Armor" (Objekt / Object)'}
    0x001B: {id: f_bb070, doc: 'Freeze'}
    0x001E: {id: f_wi060, doc: 'Gulliver-Kanone (Objekt / Object)'}
    0x001F: {id: n_bb050_btl, doc: 'Unruh / Cogsworth'}
    0x0020: {id: n_bb060_btl, doc: 'Lumiere'}
    0x0021: {id: n_bb070_btl, doc: 'Madame Pottine / Mrs. Potts'}
    0x0022: {id: n_bb080_btl, doc: 'Madame Kommode / Armoire The Wardrobe'}
    0x0023: {id: f_bb000, doc: 'Nichts / "Nothing"'}
    0x0024: {id: f_bb010, doc: 'Nichts / "Nothing"'}
    0x0025: {id: f_bb020, doc: 'Nichts / "Nothing"'}
    0x0026: {id: f_bb030, doc: 'Nichts / "Nothing"'}
    0x0027: {id: n_he010_ishi_1, doc: 'Hercules (T-Pose / T-Stance) (Dummy)'}
    0x0035: {id: f_he610, doc: 'Fels mit Ringen / "Rock with Rings" (Objekt / Object) (Dummy)'}
    0x0036: {id: f_he500, doc: 'großes goldenartige Tor / "big goldenlike Door" (Objekt / Object) (Dummy)'}
    0x0037: {id: f_he510, doc: 'großes goldenartige Tor / "big goldenlike Door" (Objekt / Object) (Dummy)'}
    0x0038: {id: f_he520, doc: 'große Unterwelt Tor / "big Underworld Door" (Objekt / Object) (Dummy)'}
    0x0039: {id: f_he530, doc: 'großes grünes Tor / "big green Door" (Objekt / Object) (Dummy)'}
    0x003A: {id: f_he580, doc: 'Ketten-Ding / "Chain-Thing" (Objekt / Object) (Dummy)'}
    0x003B: {id: f_he590, doc: 'Münzenartiges Ding auf dem Boden / "Coinlike thing on the ground" (Objekt / Object) (Dummy)'}
    0x003C: {id: f_he760, doc: 'Pech & Schwefel Pokal / Panic & Pain Cup (Objekt / Object) (Dummy)'}
    0x0047: {id: m_ex110, doc: 'Silberling / Silver Rock (Gegner! / Enemy!)'}
    0x0048: {id: m_ex120, doc: 'Saphirling / Emerald Blues (Gegner! / Enemy!)'}
    0x0049: {id: m_ex130, doc: 'Purpurling / Crimson Jazz (Gegner! / Enemy!)'}
    0x004A: {id: m_ex210, doc: 'Luftpirat / Air Pirate (Gegner! / Enemy!)'}
    0x004B: {id: m_ex590, doc: 'Freudenspender / Bulky Vendor (Gegner! / Enemy!)'}
    0x004C: {id: m_al020_fire, doc: 'Kugelfeuer / Fiery Globe (Gegner! / Enemy!)'}
    0x004D: {id: m_al020_icee, doc: 'Würfeleis / Icy Cube (Gegner! / Enemy!)'}
    0x0051: {id: b_ex110, doc: 'Axel (Twilight Town, 2. Kampf) (Boss!)'}
    0x0054: {id: p_ex100, doc: 'Sora'}
    0x0055: {id: p_ex100_btlf, doc: 'Sora (hero form/Valor form)'}
    0x0056: {id: p_ex100_magf, doc: 'Sora (way form/Wisdom form)'}
    0x0057: {id: p_ex100_trif, doc: 'Sora (master form/master form)'}
    0x0058: {id: p_ex100_ultf, doc: 'Sora (Über-Form/final form)'}
    0x0059: {id: p_ex100_htlf, doc: 'Sora (anti-form)'}
    0x005A: {id: p_ex110, doc: 'Roxas'}
    0x005B: {id: p_ex200, doc: 'Micky (with Kutte/with robe)'}
    0x005C: {id: p_ex020, doc: 'Donald'}
    0x005D: {id: p_ex030, doc: 'Goofy'}
    0x005E: {id: p_bb000, doc: 'Beast'}
    0x005F: {id: p_nm000, doc: 'Jack (Halloween Town)'}
    0x0060: {id: p_nm000_santa, doc: 'Jack (Christmas town)'}
    0x0061: {id: p_lk000, doc: 'Simba'}
    0x0062: {id: p_al000, doc: 'Aladdin'}
    0x0063: {id: p_mu000, doc: 'Mulan'}
    0x0064: {id: p_mu010, doc: 'Ping'}
    0x0065: {id: p_he000, doc: 'Auron'}
    0x0066: {id: p_ca000, doc: 'Sparrow'}
    0x0067: {id: w_ex010, doc: 'King trailer/Kingdom key (dummy)'}
    0x0068: {id: w_ex010_10, doc: 'Star loyalty/Oath Keeper (dummy)'}
    0x0069: {id: w_ex010_20, doc: 'Note Irish/Oblivion (dummy)'}
    0x006A: {id: w_ex010_y0, doc: 'Glitchy KHI Ultima (dummy)'}
    0x006B: {id: w_ex010_z0, doc: 'Glitchy KHI Ultima (dummy)'}
    0x006C: {id: w_ex020, doc: 'Magic wand (dummy)'}
    0x006D: {id: w_ex030, doc: 'Knight shield (dummy)'}
    0x006E: {id: w_ex030_z0, doc: 'Sign (dummy)'}
    0x006F: {id: w_al000, doc: 'Bent sword (dummy)'}
    0x0070: {id: w_he000, doc: 'Battle companion (dummy)'}
    0x0071: {id: w_mu000, doc: 'Ancestor sword (dummy)'}
    0x0072: {id: n_mu080_ply, doc: 'Mushu (dummy)'}
    0x0073: {id: w_ca000, doc: 'Dead head sword (dummy)'}
    0x0074: {id: w_ex200, doc: 'Mickys key sword (dummy)'}
    0x0075: {id: w_bb000, doc: 'Nothing?'}
    0x0076: {id: m_ex200, doc: 'Gigg Schack (opponent! /Enemy!) '}
    0x0077: {id: m_ex200_nm, doc: 'Gigg Schack (opponent! /Enemy!)'}
    0x0078: {id: m_ex420, doc: 'Schattenschalk (opponent! /Enemy!)'}
    0x0079: {id: m_ex600, doc: 'Tetrabot (opponent! /Enemy!)'}
    0x007A: {id: m_ex710, doc: 'Morning star (opponent! /Enemy!)'}
    0x007B: {id: m_ex730, doc: 'Tornado dancer (opponent! /Enemy!)'}
    0x007C: {id: m_ex740, doc: 'Crescendo (opponent! /Enemy!)'}
    0x007D: {id: m_ex750, doc: 'Schauderplanze (opponent! /Enemy!) '}
    0x007E: {id: b_nm000, doc: 'Oogie Boogie (Boss!)'}
    0x007F: {id: n_nm050_btl, doc: 'Fear (Boss!)'}
    0x0080: {id: n_nm060_btl, doc: 'Fear (Boss!)'}
    0x0081: {id: n_nm070_btl, doc: 'Fright (Boss!) '}
    0x0082: {id: n_al010_matsu, doc: 'Carpet/Carpet'}
    0x0087: {id: n_al050_matsu, doc: ''}
    0x0088: {id: h_al010_matsu, doc: ''}
    0x0089: {id: n_al070_matsu, doc: ''}
    0x008A: {id: h_al020_matsu, doc: ''}
    0x008E: {id: m_ex090_matsu, doc: ''}
    0x008F: {id: f_al510_matsu, doc: ''}
    0x0090: {id: f_al530_matsu, doc: 'Some kind of cart'}
    0x0091: {id: f_al550_matsu, doc: 'Gem from Agrabah'}
    0x0092: {id: f_al580_matsu, doc: 'random grey platform'}
    0x0093: {id: f_al600_matsu, doc: 'Statue that holds Gem'}
    0x0094: {id: f_al610_matsu, doc: 'Goofy'}
    0x0095: {id: f_al620_matsu, doc: 'a door of some kind'}
    0x0096: {id: f_al630_matsu, doc: 'A jar (Agrabah?)'}
    0x0097: {id: f_al640_matsu, doc: 'Genie lamp'}
    0x0098: {id: f_al650_matsu, doc: 'Top of a building (agrabah)'}
    0x0099: {id: f_al660_matsu, doc: 'a pot/jar (agrabah)'}
    0x009A: {id: f_al670_matsu, doc: ''}
    0x009B: {id: f_al680_matsu, doc: ''}
    0x009C: {id: f_al690_matsu, doc: ''}
    0x009D: {id: f_al700_matsu, doc: ''}
    0x009E: {id: f_al001_matsu, doc: ''}
    0x009F: {id: n_lm120_matsu, doc: ''}
    0x00A0: {id: n_lm130_matsu, doc: 'Ariels Sister'}
    0x00A1: {id: n_lm140_matsu, doc: 'fish playing flute'}
    0x00A2: {id: n_lm150_matsu, doc: ''}
    0x00A3: {id: n_lm160_matsu, doc: ''}
    0x00A4: {id: n_lm170_matsu, doc: ''}
    0x00A5: {id: n_lm180_matsu, doc: ''}
    0x00A6: {id: n_lm190_matsu, doc: ''}
    0x00A7: {id: n_lm200_matsu, doc: ''}
    0x00A8: {id: n_lm090_matsu, doc: ''}
    0x00B0: {id: n_nm030_matsu, doc: ''}
    0x00B5: {id: n_nm080_matsu, doc: 'TNBC Bathtub'}
    0x00B7: {id: p_ex100_npc, doc: ''}
    0x00B9: {id: p_ex020_angry_npc, doc: ''}
    0x00C8: {id: h_ex500_btlf, doc: 'Hero form/Valor form (dummy)'}
    0x00C9: {id: h_ex530, doc: 'Diz (dummy)'}
    0x00CA: {id: h_ex560, doc: 'Hooded Mickey (dummy)'}
    0x00CB: {id: h_ex590, doc: 'Pete (dummy)'}
    0x00CC: {id: h_he010, doc: ''}
    0x00CD: {id: h_he020, doc: 'Megra (dummy)'}
    0x00CE: {id: h_he030, doc: 'Auron (dummy)'}
    0x00CF: {id: h_he040, doc: 'Hades (dummy)'}
    0x00D0: {id: h_bb010, doc: 'Beast (dummy)'}
    0x00D1: {id: h_bb030, doc: 'Belle (dummy)'}
    0x00D2: {id: h_al030, doc: 'Genie (dummy)'}
    0x00D3: {id: h_al040, doc: 'Jasmin (dummy)'}
    0x00D4: {id: h_mu010, doc: 'Captain Li Shang'}
    0x00D5: {id: h_mu020, doc: 'Mulan (dummy)'}
    0x00D6: {id: h_mu030, doc: 'Ping (dummy)'}
    0x00D7: {id: h_mu040, doc: 'Mushu (dummy)'}
    0x00D8: {id: h_nm020, doc: 'Christmas Jack (dummy)'}
    0x00D9: {id: h_ca010, doc: 'Sparrow (dummy)'}
    0x00DA: {id: m_ex310_npc, doc: 'Bahemoth (dummy)'}
    0x00DB: {id: m_ex320_npc, doc: 'Wayvern (dummy)'}
    0x00DC: {id: m_ex760_npc, doc: 'Armored Knight (dummy)'}
    0x00DD: {id: m_ex770_npc, doc: 'Surveilance Robot (dummy)'}
    0x00DE: {id: f_ex510, doc: 'some kind of note.'}
    0x00DF: {id: f_ex520, doc: 'Nothing'}
    0x00E0: {id: f_ex540, doc: 'naimine doll'}
    0x00E1: {id: f_ex500, doc: 'Struggle Trophy'}
    0x00E2: {id: f_ex550, doc: 'Red struggle gem'}
    0x00E3: {id: f_ex551, doc: 'Blue struggle gem'}
    0x00E4: {id: f_ex552, doc: 'Yellow struggle gem'}
    0x00E5: {id: f_ex553, doc: 'Green struggle gem'}
    0x00E6: {id: f_ex560, doc: 'Twilight Town train'}
    0x00E7: {id: f_ex570, doc: 'Freeze'}
    0x00EC: {id: n_ex550, doc: 'Schoolgirl Kairi (dummy)'}
    0x00ED: {id: n_ex630, doc: 'Diz (dummy)'}
    0x00EE: {id: n_ex750, doc: 'Org XIII member (dummy)'}
    0x00EF: {id: n_ex760, doc: 'Pete (dummy)'}
    0x00F0: {id: n_zz010, doc: 'Org XIII member (dummy)'}
    0x0103: {id: h_zz010, doc: 'Freeze'}
    0x0104: {id: h_zz020, doc: 'Sora (Kingdom Hearts I) (Dummy)'}
    0x0105: {id: h_zz030, doc: 'Donald (Dummy)'}
    0x0106: {id: h_zz040, doc: 'Goofy (Dummy)'}
    0x0107: {id: h_zz050, doc: 'disney style goofy (dummy)'}
    0x0108: {id: h_ex500, doc: 'Sora (dummy)'}
    0x0109: {id: h_ex501, doc: 'Halloween Town Sora (dummy)'}
    0x010A: {id: h_ex520, doc: 'Axel (dummy)'}
    0x011A: {id: n_mu010_qsato, doc: 'Li Shang (T-Pose / T-Stance) (Dummy)'}
    0x011B: {id: n_mu020_qsato, doc: 'Yao (T-Pose / T-Stance) (Dummy)'}
    0x011C: {id: n_mu030_qsato, doc: 'Chien-Po (T-Pose / T-Stance) (Dummy)'}
    0x011D: {id: n_mu040_qsato, doc: 'Ling (T-Pose / T-Stance) (Dummy)'}
    0x011E: {id: n_mu050_qsato, doc: 'Der Kaiser von China / Emperor of China (T-Pose / T-Stance) (Dummy)'}
    0x011F: {id: n_mu060_qsato, doc: 'Chinesischer Soldat / Chinese Soldier (T-Pose / T-Stance) (Dummy)'}
    0x0120: {id: n_mu070_qsato, doc: 'Chinesischer Soldat 2 / Chinese Soldier 2 (T-Pose / T-Stance) (Dummy)'}
    0x0121: {id: n_mu080_qsato, doc: 'Mushu (T-Pose / T-Stance) (Dummy)'}
    0x0122: {id: n_mu090_qsato, doc: 'kleiner roter Drache / "tiny dark red Dragon" (T-Pose / T-Stance) (Dummy)'}
    0x0123: {id: f_mu510, doc: 'kleiner Drachenkopf / "tiny Dragon Head" (T-Pose / T-Stance) (Dummy)'}
    0x0124: {id: f_mu520, doc: 'Feuerwerk / "Firework" (Objekt / Object) (Dummy)'}
    0x0125: {id: f_mu550, doc: 'Shan-Yus Schwert / "Shan-Yus Sword" (Objekt / Object) (Dummy)'}
    0x0126: {id: f_mu560, doc: 'großes rotes Palasttor / "great red Palace Gate" (Objekt / Object) (Dummy)'}
    0x0127: {id: n_mu010_qsato_2, doc: 'Li Shang'}
    0x0128: {id: p_he000_npc_qsato, doc: 'Auron (NPC) (ohne Schwert / withouth Sword)'}
    0x0129: {id: w_he000_npc_qsato, doc: 'Stift? / "Pen?" (Objekt / Object) (Dummy)'}
    0x012A: {id: b_he030_npc_qsato, doc: 'Hades (seltames Verhalten / strange behavior)'}
    0x012D: {id: m_ex010, doc: 'Klappersoldat / Soldier (Gegner! / Enemy!)'}
    0x012E: {id: m_ex020, doc: 'Schattenlurch / Shadow (Gegner! / Enemy!)'}
    0x012F: {id: m_ex050, doc: 'Adowampe / Large Body (Gegner! / Enemy!)'}
    0x0130: {id: m_ex660, doc: 'Propellerdrohne / Rapid Thruster (Gegner! / Enemy!)'}
    0x0131: {id: m_ex760, doc: 'Schatten-Infanterist / Armored Knight (Gegner! / Enemy!)'}
    0x0132: {id: m_ex770, doc: 'Wachroboter / Surveillance Robot (Gegner! / Enemy!)'}
    0x0133: {id: b_ex100, doc: 'Twilight-Dorn / Twilight Thorn (Boss!)'}
    0x0134: {id: m_ex890, doc: 'Dragoner / Dragoon (Gegner! / Enemy!)'}
    0x0135: {id: m_ex900, doc: 'Meuchler / Assassin (Gegner! / Enemy!)'}
    0x0136: {id: m_ex910, doc: 'Samurai (Gegner! / Enemy!)'}
    0x0137: {id: m_ex920, doc: 'Scharfschütze / Sniper (Gegner! / Enemy!)'}
    0x0138: {id: m_ex930, doc: 'Tänzer / Dancer (Gegner! / Enemy!)'}
    0x0139: {id: m_ex940, doc: 'Berserker (Gegner! / Enemy!)'}
    0x013A: {id: m_ex950, doc: 'Spieler / Gambler (Gegner! / Enemy!)'}
    0x013B: {id: m_ex960, doc: 'Beschwörer / Sorcerer (Gegner! / Enemy!)'}
    0x013C: {id: m_ex870, doc: 'Beschwörer v2 / Sorcerer v2 (Gegner! / Enemy!)'}
    0x013D: {id: m_ex880, doc: 'Kriecher / Creeper (Gegner! / Enemy!)'}
    0x013E: {id: m_ex990, doc: 'Dämmerling / Dusk (Gegner! / Enemy!)'}
    0x013F: {id: f_ex000, doc: 'Nichts / "Nothing"'}
    0x0140: {id: f_ex030, doc: 'kleine Schatzkiste / "little treasure chest" (Das Land der Drachen / Land of the Dragons) (Objekt / Object)'}
    0x0141: {id: f_ex030_bb, doc: 'kleine Schatzkiste / "little treasure chest" (Ort des Erwachens / Station of Serenity) (Objekt / Object)'}
    0x0142: {id: f_ex030_he, doc: 'kleine Schatzkiste / "little treasure chest" (Agrabah) (Objekt / Object)'}
    0x0143: {id: f_ex030_tr, doc: 'kleine Schatzkiste / "little treasure chest" (Space Paranoids) (Objekt / Object)'}
    0x0144: {id: f_ex040, doc: 'Schatzkiste / "Treasure Chest" (Objekt / Object)'}
    0x0145: {id: f_ex040_tr, doc: 'Schatzkiste / "Treasure Chest" (Space Paranoids) (Objekt / Object)'}
    0x0146: {id: f_al000, doc: 'Freeze'}
    0x0147: {id: f_al010, doc: 'Nichts / "Nothing"'}
    0x0148: {id: f_al020, doc: 'arabisches Schild / "arabian Sign" (Objekt / Object) (Dummy)'}
    0x0149: {id: f_al040, doc: 'Schatzbox / "Treasure Box" (Objekt / Object)'}
    0x014A: {id: n_al090_btl, doc: 'Abu (mit Edelstein / with Gem)'}
    0x014B: {id: f_mu000, doc: 'zerstörbarer Fels / "destructable Rock" (Reaktionskommando / Reaction Command) (Objekt / Object)'}
    0x014C: {id: f_mu010, doc: 'zerstörbarer Fels / "destructable Rock" (Reaktionskommando / Reaction Command) (Objekt / Object)'}
    0x014D: {id: f_mu020, doc: 'zerstörbarer Fels / "destructable Rock" (Reaktionskommando / Reaction Command) (Objekt / Object)'}
    0x014E: {id: f_mu040, doc: 'Feuerwerk / "Firework" (Objekt / Object)'}
    0x014F: {id: f_mu050, doc: 'Feuerwerk (Rakete) / "Firework (Rocket)" (Objekt / Object)'}
    0x0150: {id: f_mu060, doc: 'Drive-Kugel Wagon / "Drive Orb Wagon" (Objekt / Object)'}
    0x0151: {id: f_mu070, doc: 'Windritt (Reaktionskommando / Reaction Command)'}
    0x0152: {id: f_mu080, doc: 'Nichts / "Nothing"'}
    0x0153: {id: f_mu090, doc: 'Nichts / "Nothing"'}
    0x0154: {id: f_mu100, doc: 'Nichts / "Nothing"'}
    0x0155: {id: f_mu100_shang, doc: 'Nichts / "Nothing"'}
    0x0156: {id: f_mu100_tower, doc: 'Nichts / "Nothing"'}
    0x0158: {id: f_wi000, doc: 'Funke? / "Sparkle?" (Gegner! / Enemy!)'}
    0x0159: {id: f_wi010, doc: 'Weißes Feuer / "White Fire" (Gegner! / Enemy!)'}
    0x015C: {id: b_al020, doc: 'Dschafar / Jafar (Djinn) (Boss!)'}
    0x015D: {id: b_he030_part, doc: 'Hades (1. & 2. Kampf / Battle) (Boss!)'}
    0x015E: {id: b_he030, doc: 'Hades (3. & Paradox Hades Cup-Kampf / Battle) (Boss!)'}
    0x015F: {id: b_he020, doc: 'Zerberus / Cerberus (Boss!)'}
    0x0160: {id: b_he100, doc: 'Hydra (Boss!)'}
    0x0161: {id: b_bb100, doc: 'Torwächter / Thresholder (Boss!)'}
    0x0162: {id: b_bb110, doc: 'Dunkeltroll / Dark Thorn (Boss!)'}
    0x0163: {id: b_bb120, doc: 'Kettenbalg / Shadow Stalker (Boss!)'}
    0x0164: {id: b_bb130, doc: 'Nichts / "Nothing"'}
    0x0165: {id: b_mu120, doc: 'Sturmreiter / Storm Rider (Boss!)'}
    0x0166: {id: b_ca000, doc: 'Illuminator (Boss!)'}
    0x0167: {id: n_ex760_btl, doc: 'Karlo / Pete (Fluss der Nostalgie / Timeless River) (Boss!)'}
    0x0168: {id: n_al010_btl, doc: 'Sora auf Teppich / Sora on Carpet'}
    0x0169: {id: p_he000_btl, doc: 'Auron (K.O.)'}
    0x016A: {id: n_he010_btl, doc: 'Hercules (Partner!)'}
    0x016B: {id: n_he040_btl, doc: 'Nichts / "Nothing"'}
    0x016C: {id: n_he020_btl, doc: 'Nichts / "Nothing"'}
    0x016D: {id: n_he030_btl, doc: 'Megara (anfeuernd? / cheering?)'}
    0x016E: {id: n_he030_btl_def, doc: 'Megara (gefesselt / enchained)'}
    0x016F: {id: m_bb010_sword, doc: 'Gargoyle-Ritter / Gargoyle Knight (Gegner! / Enemy!)'}
    0x0170: {id: m_bb010_ax, doc: 'Gargoyle-Krieger / Gargoyle Warrior (Gegner! / Enemy!)'}
    0x0171: {id: f_he020_a1, doc: 'Felsbrocken? / "Rock?" (Objekt / Object)'}
    0x0172: {id: f_he020_b1, doc: 'Felsbrocken? / "Rock?" (Objekt / Object)'}
    0x0173: {id: f_he020_a2, doc: 'Felsbrocken? / "Rock?" (Objekt / Object)'}
    0x0174: {id: f_he020_b2, doc: 'Felsbrocken? / "Rock?" (Objekt / Object)'}
    0x0179: {id: f_nm040_00, doc: 'Pferdekopf Statue / "Horse Head Statue" (Objekt / Object)'}
    0x017A: {id: f_nm040_10, doc: 'Alien Monument (Objekt / Object)'}
    0x017B: {id: f_nm050, doc: 'Alien Monument 2 (Objekt / Object)'}
    0x017C: {id: f_nm070, doc: 'Karussel / "Roundabout" (Objekt / Object)'}
    0x0182: {id: f_he040, doc: 'Goldene Hand mit Schwert / "Golden Hand with Sword" (Objekt / Object) (Dummy)'}
    0x0183: {id: f_he090, doc: 'Goldener Statuen Kopf / "Golden Statue Head" (Objekt / Object) (Dummy)'}
    0x0185: {id: n_bb020_tsuru, doc: 'Das Biest / The Beast (Prinz Klamotten/ Prince Outfit)'}
    0x0186: {id: n_bb010_tsuru, doc: 'Belle'}
    0x0187: {id: n_bb090_tsuru, doc: 'Belle (Ball Dress)'}
    0x0188: {id: n_bb050_tsuru, doc: 'Unruh / Cogsworth (Dummy)'}
    0x0189: {id: n_bb060_tsuru, doc: 'Lumiere (Dummy)'}
    0x018A: {id: n_bb070_tsuru, doc: 'Madame Pottine / Mrs. Potts (Dummy)'}
    0x018B: {id: n_bb040_tsuru, doc: 'Madame Kommode / Armoire The Wardrobe (Dummy)'}
    0x018C: {id: n_bb080_tsuru, doc: 'Madame Kommode / Armoire The Wardrobe'}
    0x018D: {id: n_bb080_tsuru1, doc: 'Kommode / "Wardrobe" (Objekt / Object)'}
    0x018E: {id: n_zz020_tsuru, doc: 'Kuttenmann / "Man with Coat" (Dummy)'}
    0x018F: {id: n_bb030_tsuru, doc: 'Das Biest / The Beast (Menschen Form / Human Form) (T-Pose / T-Stance) (Dummy)'}
    0x0190: {id: b_bb100_tsuru, doc: 'Torwächter Tor / "Thresholder Door" (Objekt / Object) (Dummy)'}
    0x0191: {id: h_bb010_tsuru, doc: 'Das Biest / The Beast (Dummy)'}
    0x0192: {id: h_bb030_tsuru, doc: 'Belle (Dummy)'}
    0x0193: {id: h_bb050_tsuru, doc: 'Unruh / Cogsworth'}
    0x0194: {id: h_bb060_tsuru, doc: 'Lumiere'}
    0x0195: {id: h_bb070_tsuru, doc: 'Madame Pottine / Mrs. Potts (Dummy)'}
    0x0196: {id: f_bb530, doc: 'Glocke? / "Bell?" (Objekt / Object) (Dummy)'}
    0x0197: {id: f_bb550, doc: 'Ritter Rüstung 1 / "Knight Armor" 1 (Objekt / Object) (Dummy)'}
    0x0198: {id: f_bb550_tsuru1, doc: 'Ritter Rüstung 2 / "Knight Armor" 2 (Objekt / Object) (Dummy)'}
    0x0199: {id: f_bb550_tsuru2, doc: 'Ritter Rüstung 3 / "Knight Armor" 3 (Objekt / Object) (Dummy)'}
    0x019A: {id: f_bb560, doc: 'Ritter Rüstung 4 / "Knight Armor" 4 (Objekt / Object) (Dummy)'}
    0x019B: {id: f_bb570, doc: 'Violettes Holztor / "Violet Wooden Door" (Objekt / Object) (Dummy)'}
    0x019C: {id: f_bb580, doc: 'Braunes Holztor / "Brown Wooden Door" (Objekt / Object) (Dummy)'}
    0x019D: {id: f_bb590, doc: 'Blaues Holztor / "Blue Wooden Door" (Objekt / Object) (Dummy)'}
    0x019E: {id: f_bb630, doc: 'Fenster vom Ball Raum / "Window from Ball Room" (Objekt / Object) (Dummy)'}
    0x019F: {id: f_bb640, doc: 'Großes Tor / "Great Door" (Objekt / Object) (Dummy)'}
    0x01A0: {id: f_bb660, doc: 'Blaues Holztor 2 / "Brown Wooden Door 2" (Objekt / Object) (Dummy)'}
    0x01A1: {id: f_bb700, doc: 'kleine Pyramide / "tiny Pyramid" (Objekt / Object) (Dummy)'}
    0x01A2: {id: n_hb500_tsuru, doc: '1000 Herzlosen Kampffront / "1000 Heartless Battlefront" (Objekt / Object) (Dummy)'}
    0x01A3: {id: n_hb510_tsuru, doc: '1000 Herzlosen Kampffront / "1000 Heartless Battlefront" (Animation) (Objekt / Object) (Dummy)'}
    0x01A4: {id: n_hb520_tsuru, doc: '1000 Herzlosen Kampffront / "1000 Heartless Battlefront" (T-Pose / T-Stance) (Objekt / Object) (Dummy)'}
    0x01A5: {id: n_ex650_tsuru, doc: 'Junge aus Twilight Town / "Boy from Twilight Town" (T-Pose / T-Stance) (Dummy)'}
    0x01A6: {id: n_ex660_tsuru, doc: 'ältere Frau / older Woman (T-Pose / T-Stance) (Dummy)'}
    0x01A7: {id: n_ex670_tsuru, doc: 'kleines Mädchen / little Girl (T-Pose / T-Stance) (Dummy)'}
    0x01A8: {id: n_ex680a_tsuru, doc: 'Mann mit blauem Hemd / Man with a blue Shirt (T-Pose / T-Stance) (Dummy)'}
    0x01A9: {id: n_ex680b_tsuru, doc: 'Typ mit weißem Hemd / Guy with a white shirt (T-Pose / T-Stance) (Dummy)'}
    0x01AA: {id: n_ex690a_tsuru, doc: 'Frau mit weißem Rock / Woman with a white Skirt (T-Pose / T-Stance) (Dummy)'}
    0x01AB: {id: n_ex690b_tsuru, doc: 'Mädchen mit orangem Rock / Girl with a orange Skirt (T-Pose / T-Stance) (Dummy)'}
    0x01AC: {id: n_ex710_tsuru, doc: 'alte Frau / old Woman (T-Pose / T-Stance) (Dummy)'}
    0x01AD: {id: n_ex810_tsuru, doc: 'alter Mann / old Man (T-Pose / T-Stance) ( (Dummy)'}
    0x01AE: {id: n_cm000, doc: 'Marluxia (T-Pose / T-Stance)'}
    0x01AF: {id: n_cm010, doc: 'Larxene (T-Pose / T-Stance)'}
    0x01B0: {id: n_cm020, doc: 'Lexaeus (T-Pose / T-Stance)'}
    0x01B1: {id: n_cm030, doc: 'Zexion (T-Pose / T-Stance)'}
    0x01B2: {id: n_cm040, doc: 'Vexen (T-Pose / T-Stance)'}
    0x01B3: {id: n_cm050, doc: 'Rainbow Naminé (T-Pose / T-Stance)'}
    0x01B4: {id: h_cm000, doc: 'Marluxia (T-Pose / T-Stance) (Dummy)'}
    0x01B5: {id: h_cm020, doc: 'Lexaeus (T-Pose / T-Stance) (Dummy)'}
    0x01B6: {id: h_cm040, doc: 'Vexen (T-Pose / T-Stance) (Dummy)'}
    0x01B7: {id: h_cm050_tsuru, doc: 'Naminé (T-Pose / T-Stance) (Dummy)'}
    0x01B8: {id: h_cm060, doc: 'Riku (KHI, Org. Outfit) (T-Pose / T-Stance) (Dummy)'}
    0x01B9: {id: h_cm070, doc: 'Riku (KHI) (T-Pose / T-Stance) (Dummy)'}
    0x01BA: {id: h_cm080, doc: 'Kairi (KHI) (T-Pose / T-Stance) (Dummy)'}
    0x01BB: {id: n_cm060_tsuru, doc: 'Squall / Leon (KHI) (T-Pose / T-Stance) (Dummy)'}
    0x01BC: {id: n_cm070_tsuru, doc: 'Yuffie (KHI) (T-Pose / T-Stance) (Dummy)'}
    0x01BD: {id: n_cm080_tsuru, doc: 'Aerith (KHI) (T-Pose / T-Stance) (Dummy)'}
    0x01BE: {id: n_cm090_tsuru, doc: 'Cid (KHI) (T-Pose / T-Stance) (Dummy)'}
    0x01BF: {id: n_cm100_tsuru, doc: 'Tidus (KHI) (T-Pose / T-Stance) (Dummy)'}
    0x01C0: {id: n_cm110_tsuru, doc: 'Selphi (KHI) (T-Pose / T-Stance) (Dummy)'}
    0x01C1: {id: n_cm120_tsuru, doc: 'Wakka (KHI) (T-Pose / T-Stance) (Dummy)'}
    0x01C2: {id: f_cm510, doc: 'Sternenanhänger / "Star Chain" (Objekt / Object) (Dummy)'}
    0x01C3: {id: f_cm520, doc: 'Sternenanhänger / "Star Chain" (Objekt / Object) (Dummy)'}
    0x01C4: {id: f_tr010, doc: 'Ein Terminal aus Space Paranoids / "A Terminal from Space Paranoids" (Objekt / Object)'}
    0x0236: {id: player, doc: ''}
    0x0237: {id: friend_1, doc: ''}
    0x0238: {id: friend_2, doc: ''}
    0x0239: {id: world_point, doc: ''}
    0x023A: {id: save_point, doc: ''}
    0x023B: {id: actor_sora, doc: ''}
    0x023C: {id: actor_sora_h, doc: ''}
    0x023D: {id: sora_weapon_r, doc: ''}
    0x023E: {id: sora_weapon_btlf, doc: ''}
    0x023F: {id: donald_weapon, doc: ''}
    0x0240: {id: goofy_weapon, doc: ''}
    0x0279: {id: f_nm610_ishida, doc: ''}
    0x027A: {id: f_nm550_ishida, doc: ''}
    0x027B: {id: f_nm640_ishida, doc: ''}
    0x027C: {id: f_nm660_ishida, doc: ''}
    0x027D: {id: f_nm670_ishida, doc: ''}
    0x027E: {id: f_nm700_ishida, doc: ''}
    0x027F: {id: f_nm710_ishida, doc: ''}
    0x0280: {id: f_nm720_ishida, doc: ''}
    0x0281: {id: f_nm730_ishida, doc: ''}
    0x0282: {id: f_nm740_ishida, doc: ''}
    0x0283: {id: f_nm690_ishida, doc: ''}
    0x0284: {id: f_bb050, doc: ''}
    0x0285: {id: f_bb060, doc: ''}
    0x0286: {id: f_bb080, doc: ''}
    0x0287: {id: h_po010, doc: ''}
    0x028A: {id: p_lk100, doc: 'Sora (Lion-Form)'}
    0x028B: {id: f_he570, doc: 'Boat in Underworld'}
    0x028D: {id: f_nm570_ishida, doc: '2D dress'}
    0x028E: {id: n_ex790, doc: 'Melificent (dummy)'}
    0x028F: {id: h_ex760, doc: 'Olette'}
    0x0290: {id: h_ex620, doc: 'Melificent (dummy)'}
    0x0295: {id: n_lk040_oka, doc: ''}
    0x0296: {id: n_lk060_oka, doc: ''}
    0x0297: {id: n_lk070_oka, doc: ''}
    0x0298: {id: n_lk080_oka, doc: ''}
    0x0299: {id: b_lk100_00, doc: ''}
    0x029A: {id: b_lk100_10, doc: ''}
    0x029B: {id: b_lk100_20, doc: ''}
    0x029C: {id: b_lk110, doc: ''}
    0x029D: {id: p_ex030_nm, doc: 'Goofy (Halloween Town) (Glitchy)'}
    0x029E: {id: p_ex020_nm, doc: 'Donald (Halloween Town) (Glitchy)'}
    0x02A2: {id: f_he550, doc: ''}
    0x02A4: {id: f_bb100, doc: ''}
    0x02A8: {id: n_lm070_matsu, doc: ''}
    0x02A9: {id: n_lm080_matsu, doc: ''}
    0x02AA: {id: n_lm100_matsu, doc: ''}
    0x02AB: {id: n_lm110_matsu, doc: ''}
    0x02AC: {id: n_lm210_matsu, doc: ''}
    0x02AD: {id: n_lm220_matsu, doc: ''}
    0x02AE: {id: n_lm230_matsu, doc: ''}
    0x02AF: {id: n_lm240_matsu, doc: ''}
    0x02B0: {id: n_lm250_matsu, doc: ''}
    0x02B1: {id: n_lm251_matsu, doc: ''}
    0x02B2: {id: f_nm630_ishida, doc: ''}
    0x02B3: {id: f_mu570, doc: ''}
    0x02B4: {id: n_nm120_ishida, doc: ''}
    0x02B5: {id: p_ex100_nm, doc: 'Sora (Halloween Town) (Glitchy)'}
    0x02B7: {id: f_bb110, doc: ''}
    0x02B8: {id: f_he050, doc: ''}
    0x02BA: {id: h_ex600, doc: ''}
    0x02BB: {id: w_ex010_nm, doc: ''}
    0x02BC: {id: w_ex010_10_nm, doc: ''}
    0x02BD: {id: w_ex010_20_nm, doc: ''}
    0x02BE: {id: w_ex020_nm, doc: ''}
    0x02BF: {id: w_ex030_nm, doc: ''}
    0x02C0: {id: auron_weapon, doc: ''}
    0x02C1: {id: n_di500, doc: ''}
    0x02C2: {id: f_di990, doc: ''}
    0x02C3: {id: h_ex510, doc: ''}
    0x02C7: {id: h_ex550, doc: ''}
    0x02C8: {id: h_nm010, doc: ''}
    0x02C9: {id: f_bb090, doc: ''}
    0x02CA: {id: f_wi320, doc: ''}
    0x02CB: {id: f_wi330, doc: ''}
    0x02CC: {id: f_wi340, doc: ''}
    0x02CD: {id: f_wi350, doc: ''}
    0x02CE: {id: p_bb000_btl, doc: ''}
    0x02CF: {id: h_ex740, doc: ''}
    0x02D0: {id: h_ex750, doc: ''}
    0x02D1: {id: b_mu130, doc: ''}
    0x02D2: {id: f_bb600, doc: ''}
    0x02D3: {id: f_bb690, doc: ''}
    0x02D4: {id: p_tr000, doc: 'Tron '}
    0x02D6: {id: f_bb120, doc: ''}
    0x02D7: {id: f_wi380, doc: ''}
    0x02D8: {id: f_wi390, doc: ''}
    0x02D9: {id: f_wi400, doc: ''}
    0x02DA: {id: f_wi410, doc: ''}
    0x02DB: {id: f_wi420, doc: ''}
    0x02DD: {id: f_mu030, doc: ''}
    0x02DE: {id: f_wi070, doc: ''}
    0x02DF: {id: f_wi080, doc: ''}
    0x02E0: {id: f_wi090, doc: ''}
    0x02E1: {id: f_wi100, doc: ''}
    0x02E2: {id: f_wi110, doc: ''}
    0x02E3: {id: f_wi120, doc: ''}
    0x02E4: {id: f_wi130, doc: ''}
    0x02E5: {id: f_wi140, doc: ''}
    0x02E6: {id: f_wi150, doc: ''}
    0x02E7: {id: f_wi160, doc: ''}
    0x02E8: {id: f_wi170, doc: ''}
    0x02E9: {id: f_wi180, doc: ''}
    0x02EA: {id: f_wi190, doc: ''}
    0x02EB: {id: f_wi200, doc: ''}
    0x02EC: {id: f_wi210, doc: ''}
    0x02ED: {id: f_wi220, doc: ''}
    0x02EE: {id: f_wi230, doc: ''}
    0x02EF: {id: f_wi240, doc: ''}
    0x02F0: {id: f_wi250, doc: ''}
    0x02F1: {id: f_wi260, doc: ''}
    0x02F2: {id: f_wi270, doc: ''}
    0x02F3: {id: f_wi280, doc: ''}
    0x02F4: {id: f_wi290, doc: ''}
    0x02F5: {id: f_wi300, doc: ''}
    0x02F6: {id: f_wi310, doc: ''}
    0x02F7: {id: f_wi360, doc: ''}
    0x02F9: {id: f_al050, doc: ''}
    0x02FA: {id: f_tr020, doc: ''}
    0x02FB: {id: f_tr030, doc: ''}
    0x02FC: {id: f_tt020, doc: ''}
    0x02FD: {id: f_tt030, doc: ''}
    0x02FE: {id: f_tt040, doc: ''}
    0x0300: {id: f_al030, doc: ''}
    0x0301: {id: f_po000, doc: ''}
    0x0302: {id: f_dc000, doc: ''}
    0x0303: {id: f_tr070, doc: ''}
    0x0304: {id: f_hb000, doc: ''}
    0x0309: {id: p_bb000_tsuru, doc: ''}
    0x030A: {id: n_hb040_btl, doc: ''}
    0x030D: {id: f_bb620, doc: ''}
    0x030E: {id: f_wi530, doc: ''}
    0x030F: {id: p_tr000_tsuru, doc: ''}
    0x0314: {id: w_nm000, doc: ''}
    0x0315: {id: w_tr000, doc: ''}
    0x0316: {id: w_lk000, doc: ''}
    0x0317: {id: w_eh000, doc: ''}
    0x0318: {id: p_ex220, doc: 'Micky (without robe)'}
    0x0319: {id: menu_friend_1, doc: 'Donald'}
    0x031A: {id: menu_friend_2, doc: ''}
    0x031B: {id: b_ex120, doc: ''}
    0x031C: {id: h_bb020_tsuru, doc: ''}
    0x031D: {id: h_bb040_tsuru, doc: ''}
    0x031E: {id: h_lk020, doc: ''}
    0x031F: {id: f_bb130, doc: ''}
    0x0320: {id: f_he030_s, doc: 'Small Phils Challenge barrel thing.'}
    0x0321: {id: f_he030_l, doc: 'Large Phils Challenge barrel thing.'}
    0x0323: {id: p_ex110_btlf, doc: 'Roxas (hero form/Valor form)'}
    0x0324: {id: h_lk030, doc: 'Nala (dummy)'}
    0x0325: {id: f_nm020, doc: 'Fountain from Halloween Town'}
    0x0326: {id: f_nm000, doc: 'Guilatine blade'}
    0x0327: {id: f_nm010, doc: 'Nothing'}
    0x0328: {id: f_nm030, doc: 'Swinging gates from Halloween Town'}
    0x0329: {id: f_nm560_ishida, doc: 'The Experiment (foot, dummy)'}
    0x032A: {id: f_nm620_ishida, doc: 'Christmas town Door '}
    0x032B: {id: f_nm650_ishida, doc: 'Large present'}
    0x032C: {id: f_nm680_ishida, doc: ''}
    0x032D: {id: f_nm750_ishida, doc: ''}
    0x032E: {id: f_nm760_ishida, doc: ''}
    0x0331: {id: n_bb010_rtn, doc: ''}
    0x0332: {id: f_ex030_al, doc: ''}
    0x0333: {id: f_ex030_ca, doc: ''}
    0x0334: {id: f_ex030_nm, doc: ''}
    0x0335: {id: f_ex030_mu, doc: ''}
    0x0336: {id: f_ex030_po, doc: ''}
    0x0337: {id: f_ex030_dc, doc: ''}
    0x0338: {id: f_ex030_wi, doc: ''}
    0x0339: {id: f_ex030_eh, doc: ''}
    0x033A: {id: f_ex040_bb, doc: ''}
    0x033B: {id: f_ex040_he, doc: ''}
    0x033C: {id: f_ex040_al, doc: ''}
    0x033D: {id: f_ex040_ca, doc: ''}
    0x033E: {id: f_ex040_nm, doc: ''}
    0x033F: {id: f_ex040_mu, doc: ''}
    0x0340: {id: f_ex040_po, doc: ''}
    0x0341: {id: f_ex040_dc, doc: ''}
    0x0342: {id: f_ex040_wi, doc: ''}
    0x0343: {id: f_ex040_eh, doc: ''}
    0x0344: {id: h_ex580, doc: ''}
    0x0345: {id: f_nm510_ishida, doc: 'Duck toy from Halloween Town'}
    0x0346: {id: f_nm520_ishida, doc: 'Jack in the box'}
    0x0347: {id: f_nm530_ishida, doc: ''}
    0x0351: {id: f_hb010, doc: ''}
    0x0353: {id: f_wi020, doc: ''}
    0x0357: {id: h_tr010, doc: ''}
    0x0358: {id: f_al570_matsu, doc: ''}
    0x035B: {id: po06_player, doc: ''}
    0x035C: {id: po07_player, doc: ''}
    0x035D: {id: po08_player, doc: ''}
    0x035E: {id: n_tr010_btl, doc: ''}
    0x035F: {id: f_tr050, doc: ''}
    0x0360: {id: f_tr060, doc: ''}
    0x0361: {id: b_ex200, doc: ''}
    0x0362: {id: h_ca020, doc: ''}
    0x0384: {id: w_ex000, doc: ''}
    0x0385: {id: n_tr010_tsuru, doc: ''}
    0x0386: {id: last_attacker, doc: ''}
    0x0387: {id: n_lm270_matsu, doc: ''}
    0x0388: {id: n_lm280_matsu, doc: ''}
    0x0389: {id: h_lm070, doc: ''}
    0x038A: {id: h_lm090, doc: ''}
    0x038C: {id: n_lm290_matsu, doc: ''}
    0x038F: {id: f_lm570_matsu, doc: ''}
    0x0390: {id: f_lm580_matsu, doc: ''}
    0x0391: {id: f_lm590_matsu, doc: ''}
    0x0394: {id: p_ca000_skl_npc, doc: ''}
    0x0395: {id: n_hb030_tsuru, doc: ''}
    0x0396: {id: n_lm010_ply, doc: ''}
    0x0397: {id: n_lm020_ply, doc: ''}
    0x039A: {id: b_he110, doc: ''}
    0x039C: {id: f_tr510, doc: ''}
    0x039D: {id: f_tr520, doc: ''}
    0x039E: {id: f_tr540, doc: ''}
    0x039F: {id: h_tr010_tsuru, doc: ''}
    0x03A0: {id: f_nm140, doc: ''}
    0x03A1: {id: wm_cursor, doc: ''}
    0x03A2: {id: wm_symbol_tt, doc: ''}
    0x03A3: {id: wm_symbol_hb, doc: ''}
    0x03A4: {id: wm_symbol_mu, doc: ''}
    0x03A5: {id: wm_symbol_bb, doc: ''}
    0x03A6: {id: wm_symbol_he, doc: ''}
    0x03A7: {id: wm_symbol_dc, doc: ''}
    0x03A8: {id: wm_symbol_lm, doc: ''}
    0x03A9: {id: wm_symbol_ca, doc: ''}
    0x03AA: {id: wm_symbol_nm, doc: ''}
    0x03AB: {id: wm_symbol_al, doc: ''}
    0x03AC: {id: wm_symbol_lk, doc: ''}
    0x03AE: {id: wm_gumi_01, doc: ''}
    0x03B6: {id: f_dc510, doc: ''}
    0x03B8: {id: prize_box_yellow, doc: ''}
    0x03B9: {id: prize_box_red, doc: ''}
    0x03BA: {id: prize_box_blue, doc: ''}
    0x03BB: {id: prize_box_green, doc: ''}
    0x03BC: {id: f_nm100, doc: ''}
    0x03BD: {id: f_nm110, doc: ''}
    0x03BE: {id: p_lm100, doc: ''}
    0x03C4: {id: n_tr010_btl_l, doc: ''}
    0x03C6: {id: f_nm120, doc: ''}
    0x03C7: {id: f_nm160, doc: ''}
    0x03C8: {id: f_nm180, doc: ''}
    0x03C9: {id: f_nm130, doc: ''}
    0x03DA: {id: n_ex770, doc: ''}
    0x03DB: {id: n_wi010_btl, doc: ''}
    0x03DC: {id: f_he600, doc: ''}
    0x03DE: {id: n_ex800, doc: ''}
    0x03E0: {id: n_nm050_btl_2, doc: ''}
    0x03E1: {id: n_nm060_btl_2, doc: ''}
    0x03E2: {id: n_nm070_btl_2, doc: ''}
    0x03E3: {id: h_mu050, doc: ''}
    0x03E4: {id: n_bb060_rtn, doc: ''}
    0x03E5: {id: b_ex130, doc: ''}
    0x03E6: {id: p_ex100_nm_btlf, doc: ''}
    0x03E7: {id: p_ex100_nm_magf, doc: ''}
    0x03E8: {id: p_ex100_nm_trif, doc: ''}
    0x03E9: {id: p_ex100_nm_ultf, doc: ''}
    0x03EA: {id: p_ex100_nm_htlf, doc: ''}
    0x03EB: {id: f_nm080, doc: ''}
    0x03EC: {id: f_dc580, doc: ''}
    0x03ED: {id: n_ex850, doc: ''}
    0x03EE: {id: menu_friend_3, doc: ''}
    0x03EF: {id: f_dc520, doc: ''}
    0x03F0: {id: h_nm000, doc: ''}
    0x03F1: {id: h_ex670, doc: ''}
    0x03F3: {id: f_bb680, doc: ''}
    0x03F4: {id: b_ca010_hum_npc, doc: ''}
    0x03F5: {id: b_ca010_skl_npc, doc: ''}
    0x03F7: {id: b_ca010, doc: ''}
    0x03F8: {id: f_tr080, doc: ''}
    0x03F9: {id: f_tr090, doc: ''}
    0x03FA: {id: f_tr100, doc: ''}
    0x03FB: {id: f_tr110, doc: ''}
    0x03FC: {id: f_tr120, doc: ''}
    0x03FD: {id: f_tr130, doc: ''}
    0x03FE: {id: f_mu590, doc: ''}
    0x03FF: {id: n_bb020_rtn, doc: ''}
    0x0400: {id: n_bb040_rtn, doc: 'Chip'}
    0x0401: {id: n_bb050_rtn, doc: 'Cogsworth'}
    0x0402: {id: n_bb070_rtn, doc: ''}
    0x0403: {id: n_bb080_rtn, doc: ''}
    0x0404: {id: n_bb090_rtn, doc: ''}
    0x0405: {id: p_bb000_rtn, doc: ''}
    0x0406: {id: tr_player, doc: ''}
    0x0409: {id: f_ca030_dark, doc: ''}
    0x040A: {id: f_ca030_light, doc: 'Crane from Port Royal'}
    0x040B: {id: b_al100_fire, doc: 'Volcanic Lord (Boss)'}
    0x040C: {id: b_al100_ice, doc: 'Blizzard Lord (boss)'}
    0x040D: {id: p_ca000_rtn, doc: 'Sparrow (can''t move from spot)'}
    0x040E: {id: n_ca010_rtn, doc: 'Elizabeth (can''t move from spot)'}
    0x040F: {id: n_ca020_rtn, doc: 'Will (can''t move from spot)'}
    0x0411: {id: f_he560, doc: 'Felsstück (Objekt / Object) (Dummy)'}
    0x0412: {id: f_al520_matsu, doc: ''}
    0x0413: {id: h_ca030, doc: ''}
    0x0415: {id: n_wi050_ishi, doc: ''}
    0x0418: {id: n_lm300_matsu, doc: ''}
    0x0422: {id: n_al020_rtn, doc: ''}
    0x0423: {id: f_nm170_catch, doc: ''}
    0x0424: {id: f_nm170_xl, doc: ''}
    0x0425: {id: f_nm170_l, doc: ''}
    0x0426: {id: f_nm170_m, doc: ''}
    0x0427: {id: f_nm170_s, doc: ''}
    0x0428: {id: n_po010_rtn, doc: ''}
    0x0429: {id: n_po020_rtn, doc: ''}
    0x042A: {id: n_po030_rtn, doc: ''}
    0x042B: {id: n_po050_rtn, doc: ''}
    0x042C: {id: n_po060_rtn, doc: ''}
    0x042D: {id: n_po080_rtn, doc: ''}
    0x042E: {id: f_mu530, doc: ''}
    0x042F: {id: h_ca050, doc: ''}
    0x0430: {id: f_tt600, doc: ''}
    0x0431: {id: f_tt610, doc: ''}
    0x0432: {id: f_tt790, doc: ''}
    0x0433: {id: p_ex020_rtn, doc: ''}
    0x0434: {id: p_ex030_rtn, doc: ''}
    0x0435: {id: n_lm030_rtn, doc: ''}
    0x0436: {id: n_lm050_rtn, doc: ''}
    0x0437: {id: n_lm060_rtn, doc: ''}
    0x043E: {id: h_ca040, doc: ''}
    0x043F: {id: n_mu010_rtn, doc: ''}
    0x0440: {id: n_mu020_rtn, doc: ''}
    0x0441: {id: n_mu030_rtn, doc: ''}
    0x0442: {id: n_mu040_rtn, doc: ''}
    0x0443: {id: n_mu050_rtn, doc: ''}
    0x0444: {id: n_mu060_rtn, doc: ''}
    0x0445: {id: n_mu070_rtn, doc: ''}
    0x0446: {id: n_he010_rtn, doc: ''}
    0x044D: {id: n_he020_rtn, doc: ''}
    0x044E: {id: n_he030_rtn, doc: ''}
    0x044F: {id: n_he040_rtn, doc: ''}
    0x0450: {id: n_he050_rtn, doc: ''}
    0x0451: {id: n_he060_rtn, doc: ''}
    0x0452: {id: b_he030_rtn, doc: ''}
    0x0453: {id: b_ca020, doc: 'Untoter Pirat A / Undead Pirate A (Gegner! / Enemy!)'}
    0x0454: {id: b_ca030, doc: 'Untoter Pirat B / Undead Pirate B (Gegner! / Enemy!)'}
    0x0455: {id: b_ca040, doc: 'Untoter Pirat C / Undead Pirate C (Gegner! / Enemy!)'}
    0x0456: {id: wm_symbol_eh, doc: ''}
    0x0457: {id: f_mu070_boss, doc: 'Windritt (Reaktionskommando / Reaction Command)'}
    0x0459: {id: b_lk120, doc: 'Bodenbeben / Groundshaker (Boss!)'}
    0x045A: {id: m_ex610, doc: 'Telebot / Strafer (Gegner! / Enemy!)'}
    0x045B: {id: p_ex330, doc: 'Freeze'}
    0x0471: {id: f_ca670, doc: ''}
    0x0472: {id: f_ca671, doc: ''}
    0x0473: {id: f_nm540_ishida, doc: ''}
    0x0487: {id: shop_point_sub, doc: ''}
    0x0488: {id: n_al080_rtn, doc: ''}
    0x048A: {id: n_wi010_rtn, doc: ''}
    0x048B: {id: n_ex760_rtn, doc: ''}
    0x048C: {id: n_wi020_rtn, doc: ''}
    0x048D: {id: n_wi030_rtn, doc: ''}
    0x048E: {id: n_wi040_rtn, doc: ''}
    0x048F: {id: n_dc010_rtn, doc: ''}
    0x0490: {id: n_dc020_rtn, doc: ''}
    0x0491: {id: n_dc030_rtn, doc: ''}
    0x0492: {id: n_dc040_rtn, doc: ''}
    0x0493: {id: n_dc050_rtn, doc: ''}
    0x0494: {id: n_hb030_rtn, doc: ''}
    0x0496: {id: n_nm010_rtn, doc: ''}
    0x0497: {id: n_nm020_rtn, doc: ''}
    0x0498: {id: n_nm040_rtn, doc: ''}
    0x0499: {id: n_nm050_rtn, doc: ''}
    0x049A: {id: n_nm060_rtn, doc: ''}
    0x049B: {id: n_nm070_rtn, doc: ''}
    0x049C: {id: n_nm090_rtn, doc: ''}
    0x049D: {id: n_nm100_rtn, doc: ''}
    0x049E: {id: n_nm110_rtn, doc: ''}
    0x049F: {id: h_lm040, doc: ''}
    0x04A0: {id: m_ex880_dancer, doc: ''}
    0x04A1: {id: shop_point, doc: ''}
    0x04A2: {id: p_al000_rtn, doc: ''}
    0x04A3: {id: n_al030_rtn, doc: ''}
    0x04A4: {id: n_al040_rtn, doc: ''}
    0x04A7: {id: f_ca730, doc: ''}
    0x04A8: {id: f_ca740, doc: ''}
    0x04A9: {id: f_ca741, doc: ''}
    0x04AB: {id: n_lk010_rtn, doc: 'Timon (dummy)'}
    0x04AC: {id: n_lk020_rtn, doc: ''}
    0x04AD: {id: n_lk030_rtn, doc: ''}
    0x04AE: {id: n_lk050_rtn, doc: ''}
    0x04AF: {id: n_lk120_rtn, doc: ''}
    0x04B0: {id: p_lk000_rtn, doc: ''}
    0x04B3: {id: p_mu010_rtn, doc: ''}
    0x04B4: {id: last_hitmark, doc: ''}
    0x04B5: {id: p_he000_rtn, doc: ''}
    0x04B6: {id: n_ex820, doc: ''}
    0x04B8: {id: b_tr000, doc: ''}
    0x04B9: {id: f_lm530_matsu, doc: ''}
    0x04BA: {id: h_lm060, doc: 'T-stanced Prince Guy From the Little Mermaid'}
    0x04BB: {id: n_dc010_btl, doc: ''}
    0x04BC: {id: n_po040_rtn, doc: ''}
    0x04BD: {id: n_po070_rtn, doc: ''}
    0x04BE: {id: n_po090_rtn, doc: ''}
    0x04BF: {id: n_po100_rtn, doc: ''}
    0x04C2: {id: h_lm030, doc: ''}
    0x04C3: {id: h_lm050, doc: ''}
    0x04C4: {id: m_ex020_nm, doc: ''}
    0x04C5: {id: f_ca020, doc: ''}
    0x04C6: {id: f_ca050, doc: ''}
    0x04CB: {id: n_lm010_rtn, doc: ''}
    0x04CC: {id: n_lm020_rtn, doc: ''}
    0x04CD: {id: n_lm040_rtn, doc: ''}
    0x04CE: {id: h_ex650, doc: ''}
    0x04D0: {id: p_ca000_human, doc: ''}
    0x04D1: {id: f_ca550, doc: ''}
    0x04D2: {id: f_ca650, doc: ''}
    0x04D3: {id: f_ca660, doc: ''}
    0x04D4: {id: f_ca720, doc: ''}
    0x04D5: {id: f_dc530, doc: ''}
    0x04D6: {id: f_wi510, doc: ''}
    0x04D7: {id: n_ex500_btl, doc: ''}
    0x04D8: {id: f_wi550, doc: ''}
    0x04D9: {id: f_wi500, doc: ''}
    0x04DA: {id: w_ca000_human, doc: ''}
    0x04DB: {id: f_lm600_matsu, doc: ''}
    0x04DC: {id: b_ex110_friend, doc: ''}
    0x04DD: {id: f_lm610_matsu, doc: ''}
    0x04DE: {id: f_lm700_matsu, doc: ''}
    0x04DF: {id: po06_f_zz000, doc: ''}
    0x04E0: {id: b_ca020_hum_oka, doc: ''}
    0x04E6: {id: b_ca020_skl_oka, doc: ''}
    0x04E7: {id: b_ca030_hum_oka, doc: ''}
    0x04E8: {id: b_ca030_skl_oka, doc: ''}
    0x04E9: {id: b_ca040_hum_oka, doc: ''}
    0x04EA: {id: b_ca040_skl_oka, doc: ''}
    0x04EB: {id: f_lm510_matsu, doc: ''}
    0x04EC: {id: f_lm520_matsu, doc: ''}
    0x04ED: {id: p_tr000_rtn, doc: ''}
    0x04EE: {id: n_dc060_oka, doc: ''}
    0x04EF: {id: n_dc070_oka, doc: ''}
    0x04F0: {id: n_lm260_matsu, doc: ''}
    0x04F1: {id: n_hb020_tsuru, doc: ''}
    0x04F2: {id: f_hb630, doc: ''}
    0x04F3: {id: f_hb660, doc: ''}
    0x04F4: {id: p_ex340, doc: ''}
    0x04F5: {id: p_wi030, doc: 'Goofy (Fluss der Nostalgie / Timeless River) (Glitchy)'}
    0x04FB: {id: n_ex780_rtn, doc: ''}
    0x04FC: {id: f_lm710_matsu, doc: ''}
    0x04FD: {id: n_lk090_matsu, doc: ''}
    0x04FE: {id: f_lm560_matsu, doc: ''}
    0x04FF: {id: p_ex100_al_carpet, doc: ''}
    0x0500: {id: n_hb010_rtn, doc: 'Scrooge'}
    0x0501: {id: n_hb020_huey_rtn, doc: 'Huey (can''t move from spot)'}
    0x0502: {id: n_hb020_dewey_rtn, doc: 'Dewey (can''t move from spot)'}
    0x0503: {id: n_hb020_louie_rtn, doc: 'Louie (can''t move from spot)'}
    0x0504: {id: n_hb530_rtn, doc: 'Squall / Leon (Dummy)'}
    0x0505: {id: n_hb540_rtn, doc: 'Cid (Dummy)'}
    0x0506: {id: n_hb550_rtn, doc: 'Cloud (Dummy)'}
    0x0507: {id: n_hb560_rtn, doc: 'Aerith (Dummy)'}
    0x0508: {id: n_hb570_rtn, doc: 'Tifa (Dummy)'}
    0x0509: {id: n_hb580_rtn, doc: 'Yuffie (Dummy)'}
    0x050A: {id: n_hb590_rtn, doc: 'Sephiroth (Dummy)'}
    0x050B: {id: n_hb600_rtn, doc: 'Yuna (Dummy)'}
    0x050C: {id: n_hb610_rtn, doc: 'Rikku (Dummy)'}
    0x050D: {id: n_hb620_rtn, doc: 'Paine (Dummy)'}
    0x050E: {id: p_ex210_rtn, doc: 'Mickey (with Coat) (Dummy)'}
    0x050F: {id: n_ex640_moogle_rtn, doc: 'Moogle (Dummy)'}
    0x0510: {id: n_ex650_hb_boy_a_rtn, doc: 'Boy in blue shirt (can''t move from spot)'}
    0x0512: {id: n_ex660_hb_lady_a_rtn, doc: 'Old woman in brown (can''t move from spot)'}
    0x0513: {id: n_ex670_hb_girl_a_rtn, doc: 'little Girl (Dummy)'}
    0x0514: {id: n_ex680_hb_man_a_rtn, doc: 'Man with a blue Shirt (Dummy)'}
    0x0515: {id: n_ex680_hb_man_b_rtn, doc: 'Guy with a white shirt (Dummy)'}
    0x0516: {id: n_ex680_hb_item_rtn, doc: 'Guy with a green Pullover (Dummy)'}
    0x0517: {id: n_ex690_hb_woman_a_rtn, doc: 'Woman with a white Skirt (Dummy)'}
    0x0518: {id: n_ex690_hb_woman_b_rtn, doc: 'Girl with a orange Skirt (Dummy)'}
    0x051A: {id: n_ex700_hb_weapon_rtn, doc: ''}
    0x051D: {id: n_ex500_rtn, doc: ''}
    0x051E: {id: n_ex510_rtn, doc: ''}
    0x051F: {id: n_ex520_rtn, doc: ''}
    0x0520: {id: n_ex620_rtn, doc: ''}
    0x0521: {id: n_ex610_rtn, doc: ''}
    0x0522: {id: n_ex570_rtn, doc: ''}
    0x0523: {id: n_ex580_rtn, doc: ''}
    0x0524: {id: n_ex590_rtn, doc: ''}
    0x0525: {id: n_ex600_rtn, doc: 'Setzer (Dummy)'}
    0x0526: {id: n_tt010_rtn, doc: 'Merlin (Dummy)'}
    0x0527: {id: n_tt020_rtn, doc: ''}
    0x0528: {id: n_tt030_rtn, doc: ''}
    0x0529: {id: n_tt040_rtn, doc: ''}
    0x052A: {id: n_ex650_tt_boy_a_rtn, doc: ''}
    0x052B: {id: n_ex650_tt_boy_b_rtn, doc: ''}
    0x052D: {id: n_ex670_tt_girl_a_rtn, doc: ''}
    0x052E: {id: n_ex670_tt_girl_b_rtn, doc: ''}
    0x052F: {id: n_ex680_tt_man_a_rtn, doc: ''}
    0x0530: {id: n_ex680_tt_man_b_rtn, doc: ''}
    0x0531: {id: n_ex680_tt_item_rtn, doc: ''}
    0x0532: {id: n_ex680_tt_referee_rtn, doc: ''}
    0x0533: {id: n_ex690_tt_woman_a_rtn, doc: ''}
    0x0534: {id: n_ex690_tt_woman_b_rtn, doc: ''}
    0x0539: {id: n_ex700_tt_weapon_rtn, doc: ''}
    0x053A: {id: n_ex660_tt_lady_a_rtn, doc: ''}
    0x053D: {id: n_ex710_tt_sweets_rtn, doc: ''}
    0x0541: {id: n_ex700_tt_sponsor_rtn, doc: ''}
    0x0542: {id: n_ex650_hb_protect_rtn, doc: ''}
    0x0543: {id: n_ex690_hb_acce_rtn, doc: ''}
    0x0544: {id: n_ex710_hb_old_f_a_rtn, doc: ''}
    0x0545: {id: n_ex810_hb_old_m_a_rtn, doc: ''}
    0x0546: {id: n_ex650_tt_protect_rtn, doc: ''}
    0x0547: {id: n_ex690_tt_acce_rtn, doc: ''}
    0x0548: {id: n_ex700_tt_gentl_a_rtn, doc: ''}
    0x0549: {id: n_ex700_tt_gentl_b_rtn, doc: ''}
    0x054A: {id: n_ex810_tt_old_m_a_rtn, doc: ''}
    0x054B: {id: n_ex710_tt_old_f_a_rtn, doc: ''}
    0x054C: {id: b_mu100_hood, doc: 'Shan-Yu (attackable) (Dummy)'}
    0x054D: {id: h_mu050_hood, doc: ''}
    0x054E: {id: n_ex720_rtn, doc: ''}
    0x054F: {id: n_ex730_rtn, doc: ''}
    0x0550: {id: n_ex740_rtn, doc: ''}
    0x0551: {id: n_lk010_btl, doc: ''}
    0x0552: {id: n_lk020_btl, doc: 'Timon and Pumbaa (Running)'}
    0x0553: {id: h_ex630, doc: 'Mermaid Sora (dummy)'}
    0x0554: {id: p_ex030_tr, doc: 'Goofy (Space Paranoids) (Glitchy)'}
    0x0555: {id: m_ex020_raw, doc: ''}
    0x0556: {id: m_ex760_crowd, doc: ''}
    0x0557: {id: m_ex660_crowd, doc: ''}
    0x0558: {id: m_ex660_raw, doc: ''}
    0x0559: {id: f_tr560, doc: ''}
    0x055A: {id: p_ex020_tr, doc: 'Donald (Space Paranoids) (Glitchy)'}
    0x055B: {id: h_lm080, doc: ''}
    0x055C: {id: n_nm130_ishida, doc: ''}
    0x055E: {id: w_ex030_10, doc: ''}
    0x055F: {id: w_ex030_10_nm, doc: ''}
    0x0560: {id: w_ex030_20, doc: ''}
    0x0561: {id: w_ex030_20_nm, doc: ''}
    0x0562: {id: w_ex030_30, doc: ''}
    0x0563: {id: w_ex030_30_nm, doc: ''}
    0x0564: {id: w_ex030_40, doc: ''}
    0x0565: {id: w_ex030_40_nm, doc: ''}
    0x0566: {id: w_ex030_50, doc: ''}
    0x0567: {id: w_ex030_50_nm, doc: ''}
    0x0568: {id: w_ex030_60, doc: ''}
    0x0569: {id: w_ex030_60_nm, doc: ''}
    0x056A: {id: w_ex030_70, doc: ''}
    0x056B: {id: w_ex030_70_nm, doc: ''}
    0x056C: {id: w_ex030_80, doc: ''}
    0x056D: {id: w_ex030_80_nm, doc: ''}
    0x056E: {id: w_ex030_90, doc: ''}
    0x056F: {id: w_ex030_90_nm, doc: ''}
    0x0570: {id: w_ex030_y0, doc: ''}
    0x0571: {id: f_he020_c1, doc: ''}
    0x0572: {id: f_he020_c2, doc: ''}
    0x0595: {id: h_zz020_tr, doc: ''}
    0x0596: {id: w_ex020_10, doc: ''}
    0x0597: {id: w_ex020_20, doc: ''}
    0x0598: {id: w_ex020_30, doc: ''}
    0x0599: {id: w_ex020_40, doc: ''}
    0x059A: {id: w_ex020_50, doc: ''}
    0x059B: {id: w_ex020_60, doc: ''}
    0x059C: {id: w_ex020_70, doc: ''}
    0x059D: {id: w_ex020_80, doc: ''}
    0x059E: {id: w_ex020_90, doc: ''}
    0x059F: {id: w_ex020_10_nm, doc: ''}
    0x05A0: {id: w_ex020_20_nm, doc: ''}
    0x05A1: {id: w_ex020_30_nm, doc: ''}
    0x05A2: {id: w_ex020_40_nm, doc: ''}
    0x05A3: {id: w_ex020_50_nm, doc: ''}
    0x05A4: {id: w_ex020_60_nm, doc: ''}
    0x05A5: {id: w_ex020_70_nm, doc: ''}
    0x05A6: {id: w_ex020_80_nm, doc: ''}
    0x05A7: {id: w_ex020_90_nm, doc: ''}
    0x05A8: {id: w_ex020_y0, doc: ''}
    0x05A9: {id: f_lm540_matsu, doc: ''}
    0x05AA: {id: f_lm550_matsu, doc: ''}
    0x05AB: {id: f_tt010, doc: ''}
    0x05AC: {id: f_lm620_matsu, doc: ''}
    0x05AD: {id: f_lm630_matsu, doc: ''}
    0x05AE: {id: f_lm640_matsu, doc: ''}
    0x05AF: {id: f_lm650_matsu, doc: ''}
    0x05B0: {id: f_lm670_matsu, doc: ''}
    0x05B1: {id: f_lm760_matsu, doc: 'Globe that has the word AMERICA on it'}
    0x05B2: {id: f_po630, doc: ''}
    0x05B3: {id: f_po680, doc: 'Honey Pot'}
    0x05B4: {id: f_po690, doc: ''}
    0x05B5: {id: f_hb580, doc: ''}
    0x05B6: {id: f_po510, doc: ''}
    0x05B7: {id: f_po600, doc: ''}
    0x05B8: {id: f_po640, doc: ''}
    0x05B9: {id: f_po650, doc: ''}
    0x05BA: {id: f_po660, doc: ''}
    0x05BB: {id: f_tt000, doc: ''}
    0x05BC: {id: m_ex760_raw, doc: 'Schatten-Infanterist (from 1000 Heartless Battle)'}
    0x05BD: {id: m_ex770_raw, doc: 'Wachroboter (from 1000 Heartless Battle)'}
    0x05BE: {id: f_ca060, doc: ''}
    0x05BF: {id: f_al710_matsu, doc: ''}
    0x05C0: {id: f_al720_matsu, doc: ''}
    0x05C1: {id: f_al730_matsu, doc: ''}
    0x05C2: {id: b_he000_ishi, doc: 'Giant Rock Titan (Dummy)'}
    0x05C3: {id: prize, doc: ''}
    0x05C4: {id: prize_mu, doc: ''}
    0x05C5: {id: f_lm660_matsu, doc: ''}
    0x05C6: {id: f_lm680_matsu, doc: ''}
    0x05C7: {id: f_lm690_matsu, doc: ''}
    0x05C8: {id: f_lm720_matsu, doc: ''}
    0x05C9: {id: f_lm730_matsu, doc: ''}
    0x05CA: {id: f_lm740_matsu, doc: ''}
    0x05CB: {id: f_lm750_matsu, doc: ''}
    0x05CC: {id: f_lm770_matsu, doc: ''}
    0x05CD: {id: f_lm780_matsu, doc: ''}
    0x05CE: {id: b_nm100, doc: 'Prison Keeper (Boss!)'}
    0x05CF: {id: p_wi020, doc: 'Donald (Timeless River) (Glitchy)'}
    0x05D0: {id: b_nm110, doc: 'The Experiment (Boss!)'}
    0x05D1: {id: h_ex640, doc: ''}
    0x05D2: {id: f_po590, doc: ''}
    0x05D3: {id: n_po110_ishi, doc: ''}
    0x05D4: {id: f_ca710, doc: ''}
    0x05D5: {id: f_hb650, doc: ''}
    0x05D6: {id: f_tt640, doc: ''}
    0x05D7: {id: f_tt710, doc: ''}
    0x05D8: {id: f_dc580_dc05, doc: ''}
    0x05D9: {id: f_ca040, doc: ''}
    0x05DA: {id: f_tt700, doc: ''}
    0x05DB: {id: f_tt800, doc: ''}
    0x05DC: {id: f_ca700, doc: ''}
    0x05DD: {id: h_zz030_tr, doc: ''}
    0x05DE: {id: n_ca080_oka, doc: ''}
    0x05DF: {id: n_ca090_oka, doc: ''}
    0x05E0: {id: f_tt520, doc: ''}
    0x05E1: {id: f_ca580, doc: ''}
    0x05E2: {id: f_ca590, doc: ''}
    0x05E3: {id: f_ca600, doc: ''}
    0x05E4: {id: f_ca560, doc: ''}
    0x05E5: {id: f_hb640, doc: ''}
    0x05E6: {id: b_ca060, doc: ''}
    0x05E7: {id: f_ca680, doc: ''}
    0x05E8: {id: f_ca640, doc: ''}
    0x05E9: {id: al14_player, doc: ''}
    0x05EA: {id: al14_carpet, doc: ''}
    0x05EB: {id: f_ca520, doc: ''}
    0x05EC: {id: f_ca750, doc: ''}
    0x05ED: {id: f_ca760, doc: ''}
    0x05EE: {id: f_ca770, doc: ''}
    0x05EF: {id: p_lk020, doc: 'Donald (Gull-Form)'}
    0x05F0: {id: f_hb020, doc: 'Nothing'}
    0x05F1: {id: f_hb670, doc: 'Red/Green triangle'}
    0x05F2: {id: prize_ca, doc: 'Nothing'}
    0x05F3: {id: prize_he, doc: 'Nothing'}
    0x05F4: {id: prize_tt, doc: 'Nothing.'}
    0x05F5: {id: p_ex100_tr_lightcycle, doc: 'Sora on Lightbike... minus the lightbike.'}
    0x05F6: {id: prize_po, doc: 'Nothing'}
    0x05F7: {id: n_ex880, doc: 'ORG XIII member (dummy)'}
    0x05F8: {id: b_ex150, doc: 'Luxord (WORKS! can''t be killed, or paused)'}
    0x05F9: {id: h_ex500_magf, doc: 'Wisdom form (dummy)'}
    0x05FA: {id: h_ex500_trif, doc: 'master form (dummy)'}
    0x05FB: {id: h_ex500_ultf, doc: 'final form (dummy)'}
    0x05FC: {id: h_ca060, doc: 'Barbossa (dummy)'}
    0x05FD: {id: f_hb530, doc: 'doors...'}
    0x05FE: {id: f_hb520, doc: 'Xehanort portrait'}
    0x05FF: {id: b_ex210, doc: 'Luxords card'}
    0x0600: {id: f_po610, doc: 'Digger hole'}
    0x0601: {id: m_ex950_card, doc: 'Sora transformed into card (controls both card and Sora)'}
    0x0602: {id: m_ex950_dice, doc: 'Sora transformed into die (controls both card and Sora, freezes once map is left)'}
    0x0603: {id: f_tr150, doc: 'Nothing'}
    0x0604: {id: f_wi520, doc: 'Curtained window fromTimeless River'}
    0x0605: {id: f_po520, doc: 'Piglettes house pop-up card'}
    0x0606: {id: m_ex990_rtn, doc: 'Dusk (not an ememy/dummy)'}
    0x0607: {id: b_ca050, doc: 'Freeze'}
    0x0608: {id: f_tt060, doc: 'reaction command'}
    0x060A: {id: f_ca530, doc: 'dagger (Peter Pans?)'}
    0x0611: {id: h_ex690, doc: 'Luxord with 5 cards (dummy)'}
    0x0612: {id: f_al070_fire, doc: 'Fire orbs from Agrabah'}
    0x0613: {id: f_al070_blizzard, doc: 'Blizzard orbs from Agrabah'}
    0x0614: {id: f_al070_thunder, doc: 'Lightning orbs from Agrabah'}
    0x0615: {id: f_tt690, doc: 'Gate'}
    0x0616: {id: f_tt810, doc: 'Chains from Port Royal'}
    0x0617: {id: f_ex580, doc: 'Sea salt ice cream'}
    0x0618: {id: prize_nm, doc: 'Present from halloween town'}
    0x061A: {id: f_ex040_sparrow, doc: 'Small little treasure chest (Port Royal)'}
    0x061B: {id: p_lk030, doc: 'Goofy (Turtle-Form)'}
    0x061C: {id: n_hb530_btl, doc: 'Squall / Leon (Partner!)'}
    0x061D: {id: po06_n_po010, doc: 'Freeze'}
    0x061E: {id: po06_n_po030, doc: 'Nothing'}
    0x061F: {id: h_dc010, doc: 'Queen Minnie (dummy)'}
    0x0620: {id: f_tt120, doc: 'Attackable machine'}
    0x0621: {id: f_mu600, doc: 'Small Pyramid (Dummy)'}
    0x0622: {id: b_ex140, doc: 'Xigbar (Boss!)'}
    0x0623: {id: f_al100, doc: 'Sandstorm'}
    0x0624: {id: n_ex780, doc: 'Org XIII member (dummy)'}
    0x0625: {id: f_po090, doc: 'Bees (Attackable)'}
    0x0626: {id: m_ex660_e_crowd, doc: 'Nothing'}
    0x0627: {id: m_ex800, doc: 'Bolt Tower (Enemy)'}
    0x0628: {id: m_ex800_raw, doc: 'Bolt Tower (enemy)'}
    0x0629: {id: f_tt590, doc: 'Money bag'}
    0x062A: {id: b_al100_1st, doc: 'Volcano Lord (freezes after 5 seconds)'}
    0x062B: {id: b_al100_2nd, doc: 'Blizzard Lord (freezes after 5 seconds)'}
    0x062C: {id: f_hb590, doc: 'Org XIII-like Chair'}
    0x0638: {id: f_tt510, doc: ''}
    0x0639: {id: n_al070_btl, doc: ''}
    0x063A: {id: f_al080, doc: ''}
    0x063B: {id: f_ca790, doc: ''}
    0x063C: {id: n_ex940_btl, doc: 'Riku (Boss!)'}
    0x063D: {id: n_ex840, doc: ''}
    0x063E: {id: n_ex940, doc: ''}
    0x063F: {id: f_ca690, doc: ''}
    0x0640: {id: f_al140, doc: ''}
    0x0641: {id: f_al150, doc: ''}
    0x0642: {id: f_al160, doc: ''}
    0x0643: {id: f_al170, doc: ''}
    0x0644: {id: f_nm090, doc: ''}
    0x0645: {id: f_ca780, doc: ''}
    0x0646: {id: b_ex170, doc: ''}
    0x0647: {id: n_wi010_btl_vs, doc: ''}
    0x0648: {id: f_hb600, doc: ''}
    0x0649: {id: f_hb510, doc: ''}
    0x064A: {id: f_wi010_boss, doc: ''}
    0x064C: {id: f_wi020_boss, doc: ''}
    0x064D: {id: f_lk560, doc: ''}
    0x064E: {id: f_eh520, doc: ''}
    0x064F: {id: f_tt660, doc: ''}
    0x0650: {id: f_tt670, doc: ''}
    0x0651: {id: f_tt680, doc: ''}
    0x0652: {id: f_tt780, doc: ''}
    0x0653: {id: f_tt870, doc: ''}
    0x0654: {id: f_tr140, doc: ''}
    0x0656: {id: p_ex100_tr, doc: 'Sora (Space Paranoids) (Glitchy)'}
    0x0657: {id: p_ex100_wi, doc: 'Sora (Fluss der Nostalgie / Timeless River) (Glitchy)'}
    0x0658: {id: f_ca540, doc: ''}
    0x0659: {id: f_eh530, doc: ''}
    0x065A: {id: po06_f_po040, doc: ''}
    0x065B: {id: po06_f_po050, doc: ''}
    0x065C: {id: po06_f_po060_s, doc: ''}
    0x065D: {id: po06_f_po080, doc: ''}
    0x065E: {id: po07_n_po010, doc: ''}
    0x065F: {id: po07_n_po090, doc: ''}
    0x0660: {id: po07_f_po040, doc: ''}
    0x0661: {id: po07_f_po080, doc: 'Honig-Topf / "Honey Pot" (Objekt / Object) (Dummy)'}
    0x0662: {id: po07_f_po060_s, doc: ''}
    0x0663: {id: po08_n_po010, doc: ''}
    0x0664: {id: po08_f_po060_s, doc: ''}
    0x0665: {id: po08_f_po060_m, doc: ''}
    0x0666: {id: po08_f_po060_l, doc: 'Nichts / "Nothing"'}
    0x0667: {id: po08_f_po060_x, doc: ''}
    0x0669: {id: p_ex100_tr_btlf, doc: 'Sora (Helden-Form / Valor-Form) (Space Paranoids) (Glitchy)'}
    0x066A: {id: p_ex100_wi_btlf, doc: 'Sora (Helden-Form / Valor-Form) (Fluss der Nostalgie / Timeless River) (Glitchy)'}
    0x066B: {id: p_ex100_tr_magf, doc: 'Sora (Weisen-Form / Wisdom Form) (Space Paranoids) (Glitchy)'}
    0x066C: {id: p_ex100_wi_magf, doc: 'Sora (Weisen-Form / Wisdom Form) (Fluss der Nostalgie / Timeless River) (Glitchy)'}
    0x066D: {id: p_ex100_tr_trif, doc: 'Sora (Meister-Form / Master-Form) (Space Paranoids) (Glitchy)'}
    0x066E: {id: p_ex100_wi_trif, doc: 'Sora (Meister-Form / Master-Form) (Fluss der Nostalgie / Timeless River) (Glitchy)'}
    0x066F: {id: p_ex100_tr_ultf, doc: 'Sora (Über-Form / Final-Form) (Space Paranoids) (Glitchy)'}
    0x0670: {id: p_ex100_wi_ultf, doc: 'Sora (Über-Form / Final-Form)(Fluss der Nostalgie / Timeless River) (Glitchy)'}
    0x0671: {id: p_ex100_tr_htlf, doc: 'Sora (Anti-Form) (Space Paranoids) (Glitchy)'}
    0x0672: {id: p_ex100_wi_htlf, doc: 'Sora (Anti-Form) (Fluss der Nostalgie / Timeless River) (Glitchy)'}
    0x0673: {id: h_ex570, doc: 'Xigbar (dummy)'}
    0x0674: {id: n_ex870, doc: 'Hooded Axel (dummy)'}
    0x0675: {id: n_ex580, doc: 'Raijin (dummy)'}
    0x0676: {id: n_ex590, doc: 'Fujin (dummy)'}
    0x0677: {id: f_nm150, doc: 'Nothing'}
    0x0678: {id: n_ex570_btl, doc: 'Seifer'}
    0x0679: {id: f_eh000, doc: 'Freeze'}
    0x067A: {id: f_eh010, doc: ''}
    0x067B: {id: f_eh020, doc: ''}
    0x067C: {id: f_eh030, doc: ''}
    0x067D: {id: f_eh040, doc: ''}
    0x067E: {id: f_ca570, doc: ''}
    0x067F: {id: f_ca620, doc: ''}
    0x0680: {id: f_he770, doc: 'Hades Cup Pokal / Hades Cup Trophy (Objekt / Object) (Dummy)'}
    0x0681: {id: f_he780, doc: ''}
    0x0682: {id: f_he790, doc: ''}
    0x0683: {id: f_he800, doc: ''}
    0x0684: {id: f_tt730, doc: ''}
    0x0685: {id: n_ex500_money_rtn, doc: ''}
    0x0686: {id: m_al020_fire_raw, doc: ''}
    0x0687: {id: m_al020_icee_raw, doc: ''}
    0x0688: {id: n_hb550_btl, doc: 'Cloud (ally)'}
    0x0689: {id: m_al020_fire2, doc: ''}
    0x068A: {id: m_al020_icee2, doc: ''}
    0x068B: {id: m_ex600_lc, doc: ''}
    0x068C: {id: f_al000_dummy, doc: ''}
    0x068D: {id: h_hb530, doc: ''}
    0x068E: {id: h_hb550, doc: ''}
    0x068F: {id: dead_boss, doc: ''}
    0x0690: {id: h_ex770, doc: ''}
    0x0691: {id: n_lk130_matsu, doc: ''}
    0x0692: {id: f_tt740, doc: ''}
    0x0694: {id: f_tt830, doc: ''}
    0x0695: {id: h_ex500_tr, doc: 'Sora (Space Paranoids) (T-Pose / T-Stance) (Dummy)'}
    0x0696: {id: h_ex500_tr_btlf, doc: 'Sora (Helden-Form / Valor-Form) (Space Paranoids) (T-Pose / T-Stance) (Dummy)'}
    0x0697: {id: h_ex500_tr_magf, doc: 'Sora (Weisen-Form / Wisdom Form) (Space Paranoids) (T-Pose / T-Stance) (Dummy)'}
    0x0698: {id: h_ex500_tr_trif, doc: 'Sora (Meister-Form / Master-Form) (Space Paranoids) (T-Pose / T-Stance) (Dummy)'}
    0x069A: {id: h_ex500_tr_ultf, doc: 'Sora (Über-Form / Final-Form) (Space Paranoids) (T-Pose / T-Stance) (Dummy)'}
    0x069B: {id: f_tt750, doc: ''}
    0x069C: {id: f_hb680, doc: ''}
    0x069D: {id: f_hb690, doc: ''}
    0x069E: {id: w_ex010_wi, doc: ''}
    0x069F: {id: w_ex020_wi, doc: ''}
    0x06A0: {id: w_ex030_wi, doc: ''}
    0x06A2: {id: po08_n_po020, doc: ''}
    0x06A3: {id: po08_n_po070, doc: ''}
    0x06AE: {id: n_ex860, doc: 'Organization Cloaked(T-Stance)'}
    0x06AF: {id: f_tt130, doc: 'Twilight Town Junk Sweep things(destroyed after finisher)'}
    0x06B0: {id: n_hb580_btl, doc: 'Yuffie(Ally!)'}
    0x06B1: {id: n_ex910, doc: 'Diz with face revealed.(T-Stance)'}
    0x06B2: {id: h_tt010, doc: 'Yensid (T -Srance)'}
    0x06B3: {id: n_hb570_btl, doc: 'Tifa(ally!)'}
    0x06B4: {id: p_ex100_memo, doc: 'Sora...just standing there...seems to be a dummy...'}
    0x06B5: {id: m_ex600_lc_atk, doc: 'After effect of Heartless being defeated, the heart floats up etc'}
    0x06B6: {id: m_ex600_lc_chg, doc: ''}
    0x06B7: {id: m_ex600_lc_grd, doc: ''}
    0x06B8: {id: f_ca000, doc: ''}
    0x06B9: {id: f_ca010, doc: ''}
    0x06BA: {id: n_ex760_btl_willy, doc: 'Freeze'}
    0x06BB: {id: n_ex760_btl_megara, doc: 'Pete (boss, can''t be killed)'}
    0x06BC: {id: n_ex760_btl_hercules, doc: 'Pete (boss)'}
    0x06BD: {id: f_wi560, doc: 'Timeless River turret (not solid)'}
    0x06BE: {id: last_gimmick, doc: 'Freeze'}
    0x06BF: {id: m_ex850, doc: 'Berserker weapon (Useable)'}
    0x06C0: {id: m_ex860, doc: 'Sora Die (dummy)'}
    0x06C1: {id: p_ex120, doc: 'Sora (Kingdom Hearts I) '}
    0x06C2: {id: n_po040_btl, doc: 'Eeyore (Follows you if you press Trianlge)'}
    0x06C3: {id: n_po020_btl, doc: 'Tigger (Follows you if you press Trianlge)'}
    0x06C4: {id: n_po030_btl, doc: 'Piglette (Follows you if you press Trianlge)'}
    0x06C5: {id: n_po070_btl, doc: 'Kanga (Follows you if you press Trianlge)'}
    0x06C6: {id: h_ex730, doc: 'Diz, without bandages (Dummy)'}
    0x06C7: {id: h_di500, doc: 'Selphie, school uniform (dummy)'}
    0x06C8: {id: p_ex350, doc: 'Freeze'}
    0x06C9: {id: b_ex160, doc: 'Saix (boss)'}
    0x06CA: {id: f_he020_po, doc: 'Falling crystal'}
    0x06CB: {id: h_ex660, doc: 'Xenmas (dummy)'}
    0x06CC: {id: f_ca690_btl, doc: 'Gold Medalion chest (empty) (dummy)'}
    0x06CD: {id: f_tr040, doc: 'Unused creeper thing from Space Paranoids world (dummy)'}
    0x06CE: {id: n_po010_btl, doc: 'Sora with Pooh in Honey Jar (can''t be thrown)'}
    0x06CF: {id: h_ex680, doc: 'Saix with weapon (dummy)'}
    0x06D0: {id: n_mu100, doc: 'wall of Rapid thrusters (dummy)'}
    0x06D1: {id: f_wi511, doc: 'Scene of the fire window from Timeless River (not solid)'}
    0x06D2: {id: f_wi512, doc: 'Lilliput window from Timeless River (not solid)'}
    0x06D3: {id: f_wi513, doc: 'Building Site window from Timeless River (not solid)'}
    0x06D7: {id: f_ca060_medal, doc: 'Attackable floor'}
    0x06D8: {id: f_tt620, doc: 'Train from Twilight Town'}
    0x06D9: {id: f_tt630, doc: 'Door for above'}
    0x06DA: {id: f_tt631, doc: 'Darker door for Twlight Town'}
    0x06DB: {id: p_tr010, doc: 'Skateboard'}
    0x06DC: {id: f_po080, doc: 'Floating Honey Pot'}
    0x06DD: {id: h_ex790, doc: 'Sora in KH1 clothes (dummy)'}
    0x06DE: {id: f_wi060_pete, doc: 'Timeless River turret (not solid)'}
    0x06DF: {id: f_wi360_pete, doc: 'Timeless River moving platform'}
    0x06E0: {id: m_ex620_al, doc: 'Fortuneteller (enemy)'}
    0x06E1: {id: f_wi570, doc: 'Skateboard'}
    0x06E2: {id: b_tr020, doc: 'MCP (not attackable or solid)'}
    0x06E3: {id: f_tt650, doc: 'Pencil'}
    0x06E4: {id: f_tt850, doc: 'Drawing pad '}
    0x06E5: {id: f_eh050, doc: 'Floating building from end of game (Game freezes when you press Triangle)'}
    0x06E6: {id: f_eh060, doc: 'Floating building from end of game'}
    0x06E7: {id: f_po530, doc: 'Pooh''s house pop up'}
    0x06E8: {id: f_po540, doc: 'Rabbits house pop up'}
    0x06E9: {id: f_po550, doc: 'Kanga & Roo''s house pop up'}
    0x06EA: {id: f_po560, doc: 'The spooky cave pop up'}
    0x06EB: {id: f_po570, doc: 'Stary Hill pop up'}
    0x06EC: {id: f_tt720, doc: 'Small white object'}
    0x06ED: {id: f_po030, doc: 'Nothing'}
    0x06EE: {id: m_ex790_halloween, doc: 'Graveyard (enemy)'}
    0x06EF: {id: m_ex790_halloween_nm, doc: 'Graveyard (enemy)'}
    0x06F0: {id: m_ex790_christmas, doc: 'Toy Soldier (enemy)'}
    0x06F1: {id: m_ex790_christmas_nm, doc: 'Toy Soldier (enenmy)'}
    0x06F2: {id: n_hb590_btl, doc: 'Sephiroth in Guard Stance (attackable dummy, resistant to Magic)'}
    0x06F3: {id: w_ex010_tr, doc: 'Nothing'}
    0x06F4: {id: w_ex020_tr, doc: 'Nothing'}
    0x06F5: {id: w_ex030_tr, doc: 'Nothing'}
    0x06F6: {id: f_tr570, doc: 'Nothing'}
    0x06F7: {id: f_ca800, doc: 'Nothing'}
    0x06F8: {id: f_lk530, doc: 'Nothing'}
    0x06F9: {id: f_lk540, doc: 'Rafiki''s mixing bowl'}
    0x06FA: {id: b_ex220, doc: 'Saix''s weapon (Usable)'}
    0x06FB: {id: f_lk510, doc: 'Small stones'}
    0x06FC: {id: f_lk520, doc: 'bent tree'}
    0x06FD: {id: f_lk550, doc: 'Rafiki''s bowl of red stuff'}
    0x06FE: {id: f_lk561, doc: 'Mufasa (dummy)'}
    0x0701: {id: f_tt521, doc: ''}
    0x0702: {id: f_tt522, doc: ''}
    0x0703: {id: f_tt523, doc: ''}
    0x0704: {id: f_tt524, doc: ''}
    0x0705: {id: f_tt525, doc: ''}
    0x070F: {id: f_tr160, doc: ''}
    0x0710: {id: b_nm110_head, doc: 'The Experiment (head) '}
    0x0711: {id: b_nm110_l_arm, doc: 'The Experiment (left Hand) (Boss)'}
    0x0712: {id: b_nm110_r_arm, doc: 'The Experiment (right Hand) (Boss)'}
    0x0714: {id: f_lk562, doc: 'Simba (Dummy)'}
    0x0715: {id: h_ex500_nm, doc: 'Sora (Halloween Town)(Dummy)'}
    0x0716: {id: h_ex500_nm_btlf, doc: 'Sora (Valor-Form) (Halloween Town) (Dummy)'}
    0x0717: {id: h_ex500_nm_magf, doc: 'Sora (Wisdom Form) (Halloween Town) (Dummy)'}
    0x0718: {id: h_ex500_nm_trif, doc: 'Sora (Master-Form) (Halloween Town) (Dummy)'}
    0x0719: {id: h_ex500_nm_ultf, doc: 'Sora (Final-Form) (Halloween Town) (Dummy)'}
    0x071A: {id: n_ex830, doc: ''}
    0x071B: {id: n_ex690_tt_a_skate_rtn, doc: ''}
    0x071C: {id: f_wi580, doc: ''}
    0x071D: {id: m_ex800_crowd, doc: ''}
    0x071E: {id: f_tt110, doc: ''}
    0x0723: {id: m_ex500_nm, doc: 'Trick Ghost (Halloween Town) (Enemy)'}
    0x0724: {id: m_ex540_wi, doc: 'Aeroplane (Timeless River)'}
    0x0725: {id: m_ex550_wi, doc: 'Minute Bomb (Timeless River) (Enemy)'}
    0x0726: {id: m_ex560_wi, doc: 'Hammer Frame (Timeless River) (Enemy)'}
    0x0727: {id: m_ex640_wi, doc: 'Hot Rod (Timeless River) (Enemy)'}
    0x0728: {id: m_ex650_tr, doc: 'Cannon Gun (Space Paranoids) (Enemy)'}
    0x0729: {id: m_ex700_nm, doc: 'Mole Driller (Enemy)'}
    0x072A: {id: m_ex120_nm, doc: 'Emerald Blues (Enemy)'}
    0x072B: {id: m_ex530_tr, doc: 'Bookmaster (Space Paranoids) (Enemy)'}
    0x072C: {id: m_ex420_nm, doc: 'Neoshadow (Enemy)'}
    0x072D: {id: m_ex750_nm, doc: 'Creeper Plant (Enemy)'}
    0x072E: {id: m_ex010_nm, doc: 'Soldier (Timeless River) (Enemy)'}
    0x072F: {id: m_ex010_tr, doc: 'Soldier (Space Paranoids) (Enemy)'}
    0x0730: {id: m_ex020_wi, doc: 'Shadow (Enemy)'}
    0x0731: {id: m_ex020_nm_raw, doc: 'Shadow (Enemy)'}
    0x0732: {id: m_ex020_wi_raw, doc: 'Shadow (Enemy)'}
    0x0733: {id: m_ex660_wi, doc: 'Rapid Thruster (Timless River) (Enemy)'}
    0x0735: {id: m_ex760_nm, doc: 'Nothing'}
    0x0737: {id: m_ex770_tr, doc: 'Nothing'}
    0x0739: {id: m_ex010_wi, doc: 'Soldier (Timless River)'}
    0x073A: {id: n_ex950, doc: 'Riku (Blind) (Dummy)'}
    0x073B: {id: w_ex010_w0, doc: 'Struggle-Battle-Bat (Dummy)'}
    0x073C: {id: m_ex120_tr, doc: 'Emerald Blues (Space Paranoids) (Enemy)'}
    0x0742: {id: n_ex900_rtn, doc: 'Riku In ansem Form (Dummy)'}
    0x0743: {id: n_ex560_rtn, doc: 'Kairi (dummy)'}
    0x0744: {id: f_eh550, doc: 'Nothing'}
    0x0745: {id: p_ex220_rtn, doc: 'Mickey (dummy)'}
    0x0746: {id: n_ex650_hb_item_rtn, doc: 'TT Boy (dummy)'}
    0x0747: {id: n_ex680_hb_protect_rtn, doc: 'TT Teen/Adult (dummy)'}
    0x0748: {id: n_ex650_tt_item_rtn, doc: 'Another TT Boy (dummy)'}
    0x0749: {id: n_ex680_tt_protect_rtn, doc: 'TT teen (dummy)'}
    0x074A: {id: m_ex800_e_crowd, doc: 'Nothing'}
    0x074B: {id: prize_tr, doc: 'Nothing'}
    0x074C: {id: f_tt760, doc: 'A GIANT GUMMI SHIP O.o (object)'}
    0x074D: {id: n_bb010_sit_rtn, doc: 'Sitting Belle (dummy)'}
    0x074E: {id: n_ca020_sit_rtn, doc: 'Sitting/Exhausted Will Turner (dummy)'}
    0x074F: {id: n_he010_sit_rtn, doc: 'Sitting/Exhausted Hurcules (dummy)'}
    0x0750: {id: n_nm010_sit_rtn, doc: ''}
    0x0751: {id: n_hb540_sit_rtn, doc: ''}
    0x0752: {id: f_nm570, doc: ''}
    0x0753: {id: m_ex610_raw, doc: ''}
    0x0754: {id: p_ex130, doc: ''}
    0x0756: {id: f_he020_pete, doc: ''}
    0x0757: {id: n_ex650_btl1, doc: ''}
    0x0758: {id: n_ex670_btl1, doc: ''}
    0x0759: {id: f_hb540, doc: ''}
    0x075C: {id: b_lk130, doc: 'A Very Tiny Triangle (probably from tron) Makes battle music but there is nothing to kill'}
    0x075D: {id: h_ex610, doc: 'Mickey Without Cloak (T-Stance)'}
    0x075E: {id: f_al110, doc: 'Nothing'}
    0x075F: {id: m_ex520_al, doc: 'Nothing'}
    0x0760: {id: m_ex130_al, doc: 'Fire Heartless'}
    0x0761: {id: m_ex660_al, doc: 'The Small Flying heartless (Like the ones on pride rock)'}
    0x0762: {id: n_ex680_btl1, doc: 'Generic Teen (dummy)'}
    0x0763: {id: n_ex690_btl1, doc: 'Generic Lady (dummy)'}
    0x0764: {id: p_ex230, doc: ''}
    0x0765: {id: f_eh560, doc: ''}
    0x0767: {id: b_ex180, doc: ''}
    0x0768: {id: b_ex240, doc: ''}
    0x0769: {id: w_ex010_30, doc: ''}
    0x076A: {id: w_ex010_40, doc: ''}
    0x076B: {id: w_ex010_50, doc: ''}
    0x076C: {id: w_ex010_60, doc: ''}
    0x076D: {id: w_ex010_70, doc: ''}
    0x076E: {id: w_ex010_80, doc: ''}
    0x076F: {id: w_ex010_90, doc: ''}
    0x0770: {id: w_ex010_a0, doc: ''}
    0x0771: {id: w_ex010_b0, doc: ''}
    0x0772: {id: w_ex010_c0, doc: ''}
    0x0773: {id: w_ex010_d0, doc: ''}
    0x0774: {id: w_ex010_e0, doc: ''}
    0x0775: {id: w_ex010_f0, doc: ''}
    0x0776: {id: w_ex010_g0, doc: ''}
    0x0777: {id: w_ex010_h0, doc: ''}
    0x0778: {id: w_ex010_j0, doc: ''}
    0x0779: {id: w_ex010_k0, doc: ''}
    0x077A: {id: w_ex010_m0, doc: ''}
    0x077B: {id: w_ex010_n0, doc: ''}
    0x077C: {id: w_ex010_u0, doc: ''}
    0x077D: {id: w_ex010_v0, doc: ''}
    0x077E: {id: w_ex010_30_nm, doc: ''}
    0x077F: {id: w_ex010_40_nm, doc: ''}
    0x0780: {id: w_ex010_50_nm, doc: ''}
    0x0781: {id: w_ex010_60_nm, doc: ''}
    0x0782: {id: w_ex010_70_nm, doc: ''}
    0x0783: {id: w_ex010_80_nm, doc: ''}
    0x0784: {id: w_ex010_90_nm, doc: ''}
    0x0785: {id: w_ex010_a0_nm, doc: ''}
    0x0786: {id: w_ex010_b0_nm, doc: ''}
    0x0787: {id: w_ex010_c0_nm, doc: ''}
    0x0788: {id: w_ex010_d0_nm, doc: ''}
    0x0789: {id: w_ex010_e0_nm, doc: ''}
    0x078A: {id: w_ex010_f0_nm, doc: ''}
    0x078B: {id: w_ex010_g0_nm, doc: ''}
    0x078C: {id: w_ex010_h0_nm, doc: ''}
    0x078D: {id: w_ex010_j0_nm, doc: ''}
    0x078E: {id: w_ex010_k0_nm, doc: ''}
    0x078F: {id: w_ex010_m0_nm, doc: ''}
    0x0790: {id: w_ex010_n0_nm, doc: ''}
    0x0791: {id: w_ex010_10_tr, doc: ''}
    0x0792: {id: w_ex010_20_tr, doc: ''}
    0x0793: {id: w_ex010_30_tr, doc: ''}
    0x0794: {id: w_ex010_40_tr, doc: ''}
    0x0795: {id: w_ex010_50_tr, doc: ''}
    0x0796: {id: w_ex010_60_tr, doc: ''}
    0x0797: {id: w_ex010_70_tr, doc: ''}
    0x0798: {id: w_ex010_80_tr, doc: ''}
    0x0799: {id: w_ex010_90_tr, doc: ''}
    0x079A: {id: w_ex010_a0_tr, doc: ''}
    0x079B: {id: w_ex010_b0_tr, doc: ''}
    0x079C: {id: w_ex010_c0_tr, doc: ''}
    0x079D: {id: w_ex010_d0_tr, doc: ''}
    0x079E: {id: w_ex010_e0_tr, doc: ''}
    0x079F: {id: w_ex010_f0_tr, doc: ''}
    0x07A0: {id: w_ex010_g0_tr, doc: ''}
    0x07A1: {id: w_ex010_h0_tr, doc: ''}
    0x07A2: {id: w_ex010_j0_tr, doc: ''}
    0x07A3: {id: w_ex010_k0_tr, doc: ''}
    0x07A4: {id: w_ex010_m0_tr, doc: ''}
    0x07A5: {id: w_ex010_n0_tr, doc: ''}
    0x07A6: {id: w_ex010_10_wi, doc: ''}
    0x07A7: {id: w_ex010_20_wi, doc: ''}
    0x07A8: {id: w_ex010_30_wi, doc: ''}
    0x07A9: {id: w_ex010_40_wi, doc: ''}
    0x07AA: {id: w_ex010_50_wi, doc: ''}
    0x07AB: {id: w_ex010_60_wi, doc: ''}
    0x07AC: {id: w_ex010_70_wi, doc: ''}
    0x07AD: {id: w_ex010_80_wi, doc: ''}
    0x07AE: {id: w_ex010_90_wi, doc: ''}
    0x07AF: {id: w_ex010_a0_wi, doc: ''}
    0x07B0: {id: w_ex010_b0_wi, doc: ''}
    0x07B1: {id: w_ex010_c0_wi, doc: ''}
    0x07B2: {id: w_ex010_d0_wi, doc: ''}
    0x07B3: {id: w_ex010_e0_wi, doc: ''}
    0x07B4: {id: w_ex010_f0_wi, doc: ''}
    0x07B5: {id: w_ex010_g0_wi, doc: ''}
    0x07B6: {id: w_ex010_h0_wi, doc: ''}
    0x07B7: {id: w_ex010_j0_wi, doc: ''}
    0x07B8: {id: w_ex010_k0_wi, doc: ''}
    0x07B9: {id: w_ex010_m0_wi, doc: ''}
    0x07BA: {id: w_ex010_n0_wi, doc: ''}
    0x07BB: {id: w_ex020_a0, doc: ''}
    0x07BC: {id: w_ex020_a0_nm, doc: ''}
    0x07BD: {id: w_ex020_10_tr, doc: ''}
    0x07BE: {id: w_ex020_20_tr, doc: ''}
    0x07BF: {id: w_ex020_30_tr, doc: ''}
    0x07C0: {id: w_ex020_40_tr, doc: ''}
    0x07C1: {id: w_ex020_50_tr, doc: ''}
    0x07C2: {id: w_ex020_60_tr, doc: ''}
    0x07C3: {id: w_ex020_70_tr, doc: ''}
    0x07C4: {id: w_ex020_80_tr, doc: ''}
    0x07C5: {id: w_ex020_90_tr, doc: ''}
    0x07C6: {id: w_ex020_a0_tr, doc: ''}
    0x07C7: {id: w_ex020_10_wi, doc: ''}
    0x07C8: {id: w_ex020_20_wi, doc: ''}
    0x07C9: {id: w_ex020_30_wi, doc: ''}
    0x07CA: {id: w_ex020_40_wi, doc: ''}
    0x07CB: {id: w_ex020_50_wi, doc: ''}
    0x07CC: {id: w_ex020_60_wi, doc: ''}
    0x07CD: {id: w_ex020_70_wi, doc: ''}
    0x07CE: {id: w_ex020_80_wi, doc: ''}
    0x07CF: {id: w_ex020_90_wi, doc: ''}
    0x07D0: {id: w_ex020_a0_wi, doc: ''}
    0x07D1: {id: w_ex030_a0, doc: ''}
    0x07D2: {id: w_ex030_a0_nm, doc: ''}
    0x07D3: {id: w_ex030_10_tr, doc: ''}
    0x07D4: {id: w_ex030_20_tr, doc: ''}
    0x07D5: {id: w_ex030_30_tr, doc: ''}
    0x07D6: {id: w_ex030_40_tr, doc: ''}
    0x07D7: {id: w_ex030_50_tr, doc: ''}
    0x07D8: {id: w_ex030_60_tr, doc: ''}
    0x07D9: {id: w_ex030_70_tr, doc: ''}
    0x07DA: {id: w_ex030_80_tr, doc: ''}
    0x07DB: {id: w_ex030_90_tr, doc: ''}
    0x07DC: {id: w_ex030_a0_tr, doc: ''}
    0x07DD: {id: w_ex030_10_wi, doc: ''}
    0x07DE: {id: w_ex030_20_wi, doc: ''}
    0x07DF: {id: w_ex030_30_wi, doc: ''}
    0x07E0: {id: w_ex030_40_wi, doc: 'Nothing'}
    0x07E1: {id: w_ex030_50_wi, doc: ''}
    0x07E2: {id: w_ex030_60_wi, doc: ''}
    0x07E3: {id: w_ex030_70_wi, doc: ''}
    0x07E4: {id: w_ex030_80_wi, doc: ''}
    0x07E5: {id: w_ex030_90_wi, doc: ''}
    0x07E6: {id: w_ex030_a0_wi, doc: ''}
    0x07E7: {id: f_ex554, doc: ''}
    0x07E8: {id: f_po700, doc: 'Nothing'}
    0x07E9: {id: m_ex050_wi, doc: ''}
    0x07EA: {id: n_ex730_tutorial_rtn, doc: 'Cat from Twilight Town (can''t move from spot)'}
    0x07EB: {id: n_ex650_btl2, doc: 'boy in green from Twilight Town (can''t move from spot)'}
    0x07EC: {id: n_ex670_btl2, doc: 'Girl with red skirt from TT (can''t move from spot)'}
    0x07ED: {id: n_ex680_btl2, doc: 'Impatient/dancing man in green from TT (can''t move from spot)'}
    0x07EE: {id: n_ex690_btl2, doc: 'Cheering girl in long dress from TT (can''t move from spot)'}
    0x07EF: {id: po06_f_po010, doc: 'Nothing'}
    0x07F0: {id: po06_f_po020, doc: 'Nothing'}
    0x07F1: {id: po07_f_po070_s, doc: 'Nothing'}
    0x07F2: {id: po07_f_po070_m, doc: 'Nothing'}
    0x07F3: {id: po07_f_po070_l, doc: 'Nothing'}
    0x07F5: {id: p_al010, doc: 'Freeze'}
    0x07F8: {id: f_hb030, doc: 'Nothing'}
    0x07FB: {id: b_al110, doc: 'Axels small fire floor attack'}
    0x07FC: {id: b_al120, doc: 'Vexen''s ice attack'}
    0x07FD: {id: f_tr170, doc: 'Nothing'}
    0x07FE: {id: h_ex720, doc: 'Riku/Ansem (dummy)'}
    0x07FF: {id: n_ex770_rtn, doc: 'Org XIII member (can''t move from spot)'}
    0x0800: {id: p_nm000_rtn, doc: 'Jack Skellington (not party member/dummy)'}
    0x0801: {id: f_tt580, doc: ''}
    0x0802: {id: p_ex020_tr_tsuru, doc: 'Tron-Donald (Dummy)'}
    0x0806: {id: po07_prize_s, doc: 'Nothing'}
    0x0807: {id: po07_prize_m, doc: 'Nothing'}
    0x0808: {id: po07_prize_l, doc: 'Nothing'}
    0x080A: {id: p_ca000_skl_rtn, doc: 'Sparrow (Dummy)'}
    0x080B: {id: p_ex020_nm_rtn, doc: 'Halloween Donald (Dummy)'}
    0x080C: {id: p_ex030_nm_rtn, doc: 'Halloween Goofy (Dummy)'}
    0x080D: {id: p_wi020_rtn, doc: 'Black and White Donald (Dummy)'}
    0x080E: {id: p_wi030_rtn, doc: 'Black and White Goofy (Dummy)'}
    0x080F: {id: n_lk070_e_crowd_1, doc: 'Nichts / "Nothing"'}
    0x0810: {id: n_lk070_e_crowd_2, doc: 'Nichts / "Nothing"'}
    0x0811: {id: n_lk070_e_crowd_3, doc: 'Nichts / "Nothing"'}
    0x0812: {id: b_he030_he05, doc: 'Hades (Boss!)'}
    0x0813: {id: b_mu100_rtn, doc: 'Shan-Yu (Dummy)'}
    0x0814: {id: p_mu000_rtn, doc: 'Mulan (Dummy)'}
    0x0816: {id: f_tt070, doc: 'Skateboard Checkmark (Object)'}
    0x0817: {id: f_nm721_ishida, doc: 'Present (Dummy)'}
    0x0818: {id: f_nm741_ishida, doc: 'Present (Dummy)'}
    0x0819: {id: p_eh000, doc: 'Riku (Only Guest)'}
    0x081A: {id: f_tt010_sora, doc: 'Skateboard (Functional!)'}
    0x081B: {id: n_al010_rtn, doc: 'Magic Carpet'}
    0x081C: {id: m_ex790, doc: 'Graveyard (enemy)'}
    0x081D: {id: p_ca000_human_low, doc: 'Sparrow'}
    0x081E: {id: p_ca000_low, doc: 'Sparrow'}
    0x081F: {id: b_ex170_last, doc: 'Invisible enemy present'}
    0x0820: {id: n_ex560, doc: 'Kairi (dummy)'}
    0x0821: {id: f_tt140, doc: 'Sword (from KH''s dive into heart)'}
    0x0822: {id: f_tt150, doc: 'Shield (from KH''s dive into heart)'}
    0x0823: {id: f_tt160, doc: 'Wand (from KH''s dive into heart)'}
    0x0824: {id: w_ex010_u0_rtn, doc: 'Struggle sword'}
    0x0825: {id: w_ex010_v0_rtn, doc: 'Struggle wand'}
    0x0826: {id: w_ex010_w0_rtn, doc: 'Struggle Shield'}
    0x0827: {id: p_lk020_rtn, doc: 'Donald (Gull-form, doesn''t move)'}
    0x0828: {id: p_lk030_rtn, doc: 'Goofy (Turtle-form, doesn''t move)'}
    0x0829: {id: f_tt860, doc: 'Building from Twilight-town'}
    0x082A: {id: b_lk100_00_rtn, doc: 'Shenzi (doesn''t move)'}
    0x082B: {id: b_lk100_10_rtn, doc: 'Banzai (doesn''t move)'}
    0x082C: {id: b_lk100_20_rtn, doc: 'Ed (doesn''t move)'}
    0x082D: {id: h_ex710, doc: 'Riku (dummy)'}
    0x082E: {id: h_ex780, doc: 'Riku in cloak with Oblivion keyblade (dummy)'}
    0x082F: {id: n_ex600_btl, doc: 'Setzer (good source of HP orbs)'}
    0x0830: {id: f_eh100, doc: 'Door/barrier in The World that Never Was'}
    0x0831: {id: n_lk110_matsu, doc: 'Pete (Lion form, dummy)'}
    0x0832: {id: p_ex360, doc: 'Freeze'}
    0x0833: {id: b_lk110_phantom, doc: 'Scar Ghost'}
    0x0838: {id: f_tt100, doc: 'Trashcan'}
    0x0839: {id: f_al090_01, doc: 'Falling pillar from Agrabah'}
    0x083A: {id: f_al090_02, doc: 'Falling pillar from Agrabah'}
    0x083B: {id: f_al090_03, doc: 'Falling pillar from Agrabah'}
    0x083C: {id: f_eh540, doc: 'Door to final battle'}
    0x083D: {id: f_eh570, doc: 'Orange wall'}
    0x083E: {id: f_eh580, doc: 'Pink wall'}
    0x083F: {id: b_ex270, doc: 'Hover bike/jet (unusable)'}
    0x0840: {id: p_ex100_sidecar, doc: 'Sora (Attack stance, but turns dummy when you jump)'}
    0x0841: {id: b_ex270_sidecar, doc: 'Hover bike/jet (unusable)'}
    0x0842: {id: b_ex280, doc: 'Spike in the ground (Someone''s weapon)'}
    0x0843: {id: b_ex290, doc: 'Xigbar''s weapon'}
    0x0844: {id: b_ex300, doc: 'Nothing(?)'}
    0x0845: {id: b_ex310, doc: 'Xaldin''s weapons'}
    0x0846: {id: b_ex320, doc: 'Axel''s weapon'}
    0x0847: {id: b_lk100, doc: 'Shenzi'}
    0x0848: {id: n_mu060_e_crowd, doc: 'nothing(?)'}
    0x0849: {id: n_ex690_tt_tutor_rtn, doc: 'lady from Twilight town'}
    0x084A: {id: h_ex700, doc: 'kairi (dummy)'}
    0x084B: {id: f_tt170, doc: 'flying attack balls'}
    0x084C: {id: b_ex110_rtn, doc: 'Axel (doesn''t move)'}
    0x084D: {id: b_ex170_rtn, doc: 'Xehanort powering up (doesn''t move)'}
    0x084E: {id: p_ex020_tr_rtn, doc: 'Donald (Tron-form, doesn''t move)'}
    0x084F: {id: p_ex030_tr_rtn, doc: 'Goofy (Tron-form, doesn''t move)'}
    0x0850: {id: f_tt600_10, doc: 'Magic Train'}
    0x0851: {id: f_tt620_10, doc: 'Twilight Town train'}
    0x0852: {id: n_nm030_rtn, doc: 'Zero (doesn''t move)'}
    0x0853: {id: f_tt611, doc: 'Magic Train door'}
    0x0854: {id: f_ex050, doc: 'Lion king treasure chest'}
    0x0855: {id: f_ex060, doc: 'Large lion king treasure chest'}
    0x0856: {id: f_he030_s_free, doc: 'Phils attack thing'}
    0x0857: {id: f_he030_l_free, doc: 'Phils attack thing (large)'}
    0x0858: {id: b_ex340, doc: 'nothing'}
    0x0859: {id: prize_colosseum, doc: 'Nothing'}
    0x085A: {id: f_po090_tt, doc: 'bees'}
    0x085B: {id: f_po090_etc, doc: 'bees'}
    0x085C: {id: b_ex260, doc: 'Xenmas in armor/sitting'}
    0x085D: {id: p_nm000_santa_rtn, doc: 'Jack Skellington (Christmas form, doesn''t move)'}
    0x085E: {id: f_ex030_nm_xmas, doc: 'Treasure Chest (Christmas Town)'}
    0x085F: {id: f_ex040_nm_xmas, doc: 'Large Treasure Chest (Christmas Town)'}
    0x0860: {id: b_ex250, doc: 'Nothing (freezes if you try to fly)'}
    0x0861: {id: p_eh010, doc: 'Orange/green flying triangle'}
    0x0862: {id: wm_cockpit, doc: 'freeze'}
    0x0863: {id: wm_hangar, doc: 'freeze'}
    0x0864: {id: f_hb700, doc: 'Red/Green triangle'}
    0x0865: {id: n_ex650_btl10, doc: 'Small boy from Twilight Town (can''t move from spot)'}
    0x0866: {id: n_ex670_btl10, doc: 'Small girl from Twilight Town (can''t move from spot)'}
    0x0867: {id: n_ex680_btl10, doc: 'Teen aged guy from Twilight Town (can''t move from spot)'}
    0x0868: {id: n_ex690_btl10, doc: 'Cheering woman from Twilight Town (can''t move from spot)'}
    0x0869: {id: p_ex210, doc: 'Mickey in robes (can be controled, dummy)'}
    0x086A: {id: wm_dc030, doc: 'freeze'}
    0x086B: {id: wm_dc040, doc: 'freeze'}
    0x086C: {id: n_ex610_btl, doc: 'Vivi (attackable, freezes when killed)'}
    0x086D: {id: p_eh000_rtn, doc: 'Riku (can''t move from spot)'}
    0x086E: {id: f_tt650_10, doc: 'Namine''s pencil (dummy)'}
    0x086F: {id: n_ex920, doc: 'Xehanort (in white coat, dummy)'}
    0x0870: {id: f_he810, doc: 'Kairi''s Letter'}
    0x0871: {id: h_he010_god, doc: 'Hercules (Dummy)'}
    0x0872: {id: n_po010_sit_rtn, doc: 'Shrunken head Pooh (Kinda funny to see, dummy)'}
    0x0873: {id: f_hb650_rtn, doc: 'Cloud (Buggy)'}
    0x0874: {id: f_po080_rtn, doc: 'Honey pot'}
    0x0875: {id: n_nm050_btl_toy, doc: 'Nothing (Freezes when paused)'}
    0x0876: {id: n_nm060_btl_toy, doc: 'Nothing (Freezes when paused)'}
    0x0877: {id: n_nm070_btl_toy, doc: 'Nothing (Freezes when paused)'}
    0x0878: {id: p_ex120_npc_red, doc: 'Red KH1 Sora (Dummy)'}
    0x0879: {id: p_ex120_npc_green, doc: 'Green KH1 Sora (Dummy)'}
    0x087A: {id: p_ex120_npc_blue, doc: 'Blue KH1 Sora (Dummy)'}
    0x087B: {id: f_tt110_free, doc: 'Freeze'}
    0x087C: {id: f_tt100_free, doc: 'Trashcan'}
    0x087D: {id: f_eh080, doc: 'Random explosions (there''s an explanation at the top of the screen)'}
    0x087F: {id: h_he010_weak, doc: 'Hercules (Dummy)'}
    0x0880: {id: f_ex590, doc: 'Roxas in Pajamas (Attackable, dummy)'}
    0x0881: {id: f_ex600, doc: 'Random explosions (there''s an explanation at the top of the screen)'}
    0x0882: {id: shop_point_low, doc: 'Moogle Shop (can''t be used)'}
    0x0883: {id: prize_box_nm, doc: 'Present (bounces away)'}
    0x0884: {id: f_eh070, doc: 'Random explosions (there''s an explanation at the top of the screen)'}
    0x0885: {id: f_wi380_rtn, doc: 'Timeless River steamboat with Corner stone in cage (not solid)'}
    0x0886: {id: f_wi390_rtn, doc: 'Freeze'}
    0x0887: {id: n_ex930, doc: 'Ansem The Wise (dummy)'}
    0x0888: {id: m_ex500_gm, doc: 'Trick Ghost (dummy, freezes instantly)'}
    0x0889: {id: m_ex790_gm, doc: 'Graveyard (dummy, freezes instantly)'}
    0x088A: {id: b_bb100_gm, doc: 'Thresholder (dummy, freezes instantly)'}
    0x088B: {id: b_bb130_gm, doc: 'Possessor (dummy, freezes instantly)'}
    0x088C: {id: b_bb120_gm, doc: 'Shadow Stalker (dummy, freezes instantly)'}
    0x088D: {id: b_ex330, doc: 'Freezes after Sora automaticly takes attack stance'}
    0x088E: {id: p_ex110_npc_pajamas, doc: 'Roxas in Pajamas (Attackable, dummy)'}
    0x088F: {id: wm_ex100, doc: 'Freeze'}
    0x0890: {id: wm_ex020, doc: 'Nothing (freezes if you leave the area)'}
    0x0891: {id: wm_ex030, doc: 'Nothing (freezes if you leave the area)'}
    0x0892: {id: p_ex100_wm, doc: 'Nothing (freezes if you leave the area)'}
    0x0893: {id: p_ex020_wm, doc: 'Nothing (freezes if you leave the area)'}
    0x0894: {id: p_ex030_wm, doc: 'Nothing (freezes if you leave the area)'}
    0x0895: {id: n_dc030_wm, doc: 'Nothing (freezes if you leave the area)'}
    0x0896: {id: n_dc040_wm, doc: 'Nothing (freezes if you leave the area)'}
    0x0897: {id: prize_ca_gambler, doc: 'Nothing (freezes if you leave the area)'}
    0x0898: {id: prize_ca_sea, doc: 'Nothing (freezes if you leave the area)'}
    0x089A: {id: eh_g_ex250, doc: 'Nobodies used to whack into Xenmas machine core (can he attacked)'}
    0x089B: {id: p_eh000_sidecar, doc: 'Riku, Riding Stance (Can replace Sora, but can''t move from spot)'}
    0x089C: {id: n_ex500_raw_rtn, doc: 'Heyner (can''t move from spot)'}
    0x089D: {id: n_ex510_raw_rtn, doc: 'Pence (can''t move from spot)'}
    0x089E: {id: n_ex520_raw_rtn, doc: 'Olette (can''t move from spot)'}
    0x089F: {id: n_ex610_raw_rtn, doc: 'Vivi (can''t move from spot)'}
    0x08A0: {id: n_ex570_raw_rtn, doc: 'Seifer (can''t move from spot)'}
    0x08A1: {id: n_ex600_raw_rtn, doc: 'Setzer (can''t move from spot)'}
    0x08A2: {id: n_ex680_tt_protect_raw_rtn, doc: 'Freeze'}
    0x08A3: {id: n_ex680_tt_referee_raw_rtn, doc: 'Freeze'}
    0x08A4: {id: n_ex690_tt_acce_raw_rtn, doc: 'Freeze'}
    0x08A5: {id: n_ex700_tt_weapon_raw_rtn, doc: 'Freeze'}
    0x08A6: {id: n_ex710_tt_sweets_raw_rtn, doc: 'Freeze'}
    0x08A7: {id: h_ex510_pajamas, doc: 'Roxas in Pajamas (dummy)'}
    0x08A8: {id: h_ex990, doc: 'Dusk (dummy)'}
    0x08A9: {id: f_tt880, doc: 'Area outside Twilight Town train station'}
    0x08AA: {id: n_ex680_tt_prt_raw_rtn, doc: 'Guy in brown pants from Twilight Town (can''t move from spot)'}
    0x08AB: {id: n_ex680_tt_ref_raw_rtn, doc: 'Guy in purple pants from Twilight Town (can''t move from spot)'}
    0x08AC: {id: n_ex690_tt_acc_raw_rtn, doc: 'Girl with pink dress from Twilight Town (can''t move from spot)'}
    0x08AD: {id: n_ex700_tt_wpn_raw_rtn, doc: 'Man with goatie from Twilight Town (can''t move from spot)'}
    0x08AE: {id: n_ex710_tt_swt_raw_rtn, doc: 'Old lady from Twilight Town (can''t move from spot)'}
    0x08AF: {id: n_tt010_sit_rtn, doc: 'Yensid (sitting, can''t move from spot)'}
    0x08B0: {id: b_ex230, doc: 'Game acts as if an enemy is present'}
    0x08B2: {id: b_ex350, doc: 'large platform from TWTNW with pink triangle sticking out of it (Donald tries to destroy it)'}
    0x08B3: {id: f_eh110, doc: 'Prevents you from leaving current area.'}
    0x08B4: {id: n_ex700_tt_spo_raw_rtn, doc: 'fat guy from Twilight Town (can''t move from spot)'}
    0x08B5: {id: b_ex110_skirmish, doc: 'Axel (boss, freezes when Reaction command is used)'}
    0x08B6: {id: n_hb630, doc: 'Sephiroth!'}
    0x08B7: {id: wm_checker, doc: 'Nothing'}
    0x08B8: {id: f_tr580_gm, doc: 'MCP face (freezes instantly)'}
    0x08B9: {id: n_po030_air_rtn, doc: 'Piglet (can''t move from spot)'}
    0x08BB: {id: f_ex030_tt, doc: 'Open treasure chest (Small)'}
    0x08BC: {id: f_ex040_tt, doc: 'Open treasure chest (Large)'}
    0x08BD: {id: f_eh510, doc: 'Kairi''s Keyblade (dummy)'}
    0x08BE: {id: m_ex800_mu, doc: 'Bolt Tower'}
    0x08BF: {id: m_ex800_mu_raw, doc: 'Bolt Tower'}
    0x08C0: {id: f_tt010_nm, doc: 'Hollow Bastion design Skateboard (works, freezes when paused)'}
    0x08C1: {id: f_tt010_wi, doc: 'Timeless River design skateboard (works, freezes when paused)'}
    0x08C2: {id: f_tt010_al, doc: 'Agrabah design skateboard (works, freezes when paused)'}
    0x08C3: {id: f_tt010_he, doc: 'Olympus Coliseum design skateboard (works, freezes when paused)'}
    0x08C4: {id: f_tt010_ca, doc: 'Port Royal design skateboard (works, freezes when paused)'}
    0x08C5: {id: f_tt010_tr, doc: 'Space Paranoids design skateboard (works, freezes when paused)'}
    0x08C6: {id: b_lk120_gm, doc: '??? (dummy, freezes instantly)'}
    0x08C7: {id: eh_g_ex290, doc: 'Nobody that shoots lazers at the end of the game. (can''t be attacked, flies away)'}
    0x08C8: {id: eh_g_ex320, doc: '??? (freezes instantly)'}
    0x08CB: {id: m_ex990_e_crowd, doc: ''}
    0x08CC: {id: n_ca010_gm, doc: ''}
    0x08CD: {id: n_ca020_gm, doc: ''}
    0x08CE: {id: n_ex740_tt_skate_rtn, doc: ''}
    0x08CF: {id: n_hb520_tsuru_s, doc: ''}
    0x08D0: {id: n_ex610_btl2, doc: 'Vivi (freezes game when killed)'}
    0x08D1: {id: p_eh000_last, doc: 'Riku'}
    0x08D2: {id: p_ex100_last, doc: 'Dieing Sora (when Xenmas electrifies him)'}
    0x08D4: {id: p_ex030_tr_tsuru, doc: 'Goofy (Tron-form, dummy)'}
    0x08D5: {id: eh_g_ex120, doc: 'Nothing'}
    0x08D6: {id: n_lm031_matsu, doc: 'Ariel (dummy)'}
    0x08D7: {id: h_lm031_matsu, doc: 'Ariel (dummy)'}
    0x08D8: {id: n_hb040_tsuru, doc: 'Stitch (dummy)'}
    0x08D9: {id: b_al100_fire_gm, doc: 'Volcano Lord (dummy, freezes instantly)'}
    0x08DA: {id: b_al100_ice_gm, doc: 'Blizzard Lord (dummy, freezes instantly)'}
    0x08DB: {id: b_ca050_gm, doc: 'Grim Reaper (dummy, freezes instantly)'}
    0x08DC: {id: b_nm100_gm, doc: 'Prison Keeper (dummy, freezes instantly)'}
    0x08DD: {id: b_mu120_gm, doc: 'Storm Rider (dummy, freezes instantly)'}
    0x08DE: {id: b_ex140_gm, doc: 'Xigbar (dummy, freezes instantly)'}
    0x08DF: {id: b_ex160_gm, doc: 'Siax (dummy, freezes instantly)'}
    0x08E0: {id: b_he020_gm, doc: 'Something.. Possibly Cerberus (dummy, freezes instantly)'}
    0x08E1: {id: b_he030_gm, doc: 'Hades (dummy, freezes instantly)'}
    0x08E2: {id: b_he100_gm, doc: 'Hydra (dummy, freezes instantly)'}
    0x08E3: {id: b_nm110_gm, doc: 'The Experiment (dummy, freezes instantly)'}
    0x08E4: {id: p_ca000hum_gm, doc: 'Sparrow (dummy, freezes instantly)'}
    0x08E5: {id: b_ca010_hum_gm, doc: 'Barbosa (dummy, freezes instantly)'}
    0x08E6: {id: b_ex130_gm, doc: 'Xaldin (dummy, freezes instantly)'}
    0x08E7: {id: n_cm040_gm, doc: 'Vexen (dummy, freezes instantly)'}
    0x08E8: {id: b_ex110_gm, doc: 'Axel (dummy, freezes instantly)'}
    0x08E9: {id: b_ex120_gm, doc: 'Demyx (dummy, freezes instantly)'}
    0x08EA: {id: n_he010_gm1, doc: 'Hercules (dummy, Freezes instantly)'}
    0x08EB: {id: n_he010_gm2, doc: 'Hercules (dummy, Freezes instantly)'}
    0x08EC: {id: n_he020_gm, doc: 'Phil (dummy, Freezes instantly)'}
    0x08ED: {id: eh_g_ex250_fly, doc: 'small flying nameless nobody'}
    0x08EE: {id: b_ex210_eh, doc: 'Luxord''s attacking card.'}
    0x08EF: {id: f_hb630_01, doc: 'Door from Ansems room'}
    0x08F0: {id: m_ex800_dc, doc: 'Bolt Tower'}
    0x08F1: {id: n_hb590_gm, doc: 'Sephiroth (dummy, Freezes instantly)'}
    0x08F2: {id: n_hb530_boss, doc: 'Leon (ally)'}
    0x08F3: {id: n_hb550_boss, doc: 'Cloud (ally)'}
    0x08F4: {id: n_hb570_boss, doc: 'Tifa (ally)'}
    0x08F5: {id: n_hb580_boss, doc: 'Yuffie (ally)'}
    0x08F6: {id: m_ex880_dancer_eh, doc: 'Demyx''s water minion'}
    0x08F7: {id: b_ex120_hb, doc: 'Demyx (Boss)'}
    0x08F8: {id: n_hb530_btl2, doc: 'Leon (Boss)'}
    0x08F9: {id: n_hb550_btl2, doc: 'Cloud (Boss)'}
    0x08FA: {id: n_hb570_btl2, doc: 'Tifa (Boss)'}
    0x08FB: {id: n_hb580_btl2, doc: 'Yuffie (Boss)'}
    0x08FC: {id: n_tr580_gm, doc: 'Blue MCP face (Freezes instantly)'}
    0x08FD: {id: p_ca000_kaji_rtn, doc: 'Sparrow (can''t move from spot)'}
    0x08FE: {id: n_ca020_kaji_rtn, doc: 'Will Turner (can''t move from spot)'}
    0x0900: {id: n_ex640_shop_rtn, doc: 'Moogle (can''t move from spot)'}
    0x0901: {id: h_ex620_nm, doc: 'Malificent (dummy)'}
    0x0902: {id: n_ex790_nm, doc: 'Malificent (dummy)'}
    0x0903: {id: n_ex670_tt_a_skate_rtn, doc: 'Small girl with tie from Twilight Town (can''t move from spot)'}
    0x0904: {id: n_ex621, doc: 'Namine (dummy)'}
    0x0905: {id: n_ex680_tt_b_skate_rtn, doc: 'Man in green from Twilight Town (can''t move from spot)'}
    0x0906: {id: n_ex650_tt_b_skate_rtn, doc: 'Boy in green from Twilight Town (can''t move from spot)'}
    0x0907: {id: f_tt030_etc, doc: 'Cart with large bag from Twilight Town'}
    0x0908: {id: p_ca000_kaji_is_rtn, doc: 'Sparrow steering ship (can''t move from spot)'}
    0x0909: {id: p_ca000_kaji_bp_rtn, doc: 'Sparrow steering ship (can''t move from spot)'}
    0x090A: {id: p_ca000_kaji_skl_rtn, doc: 'Skelleton Sparrow stearing ship (can''t move from spot)'}
    0x090B: {id: p_ex020_nobg_rtn, doc: 'Donald (can''t move from spot)'}
    0x090C: {id: p_ex030_nobg_rtn, doc: 'Goofy (can''t move from spot)'}
    0x090D: {id: m_ex990_zipper_rtn, doc: 'Dusk (can''t move from spot)'}
    0x090E: {id: b_he030_clsm, doc: 'Hades (boss)'}
    0x090F: {id: n_ex760_btl_clsm, doc: 'Pete (boss)'}
    0x0910: {id: n_he010_btl_clsm, doc: 'Hercules (boss)'}
    0x0911: {id: n_hb550_stand_rtn, doc: 'Cloud (can''t move from spot)'}
    0x0912: {id: b_ex360, doc: 'Invisible enemy, can''t be hurt.'}
    0x0913: {id: f_ca710_rtn, doc: 'Sparrow''s compass'}
    0x0914: {id: n_he020_menu_rtn, doc: 'Phil (can''t move from spot)'}
    0x0915: {id: n_bb050_sad_rtn, doc: 'Cogsworth (can''t move from spot)'}
    0x0916: {id: n_ca010_sad_rtn, doc: 'Elizabeth (can''t move from spot)'}
    0x0917: {id: n_ex500_anger_rtn, doc: 'Heyner (can''t move from spot)'}
    0x0918: {id: h_ex660_last, doc: 'Final Xenmas (dummy)'}
    0x0919: {id: n_ex580_raw_rtn, doc: 'Raijin (can''t move from spot)'}
    0x091A: {id: n_ex590_raw_rtn, doc: 'Fujin (can''t move from spot)'}
    0x091B: {id: f_po511, doc: 'Giant ''Winnie the Pooh'' book (no illustration of cover)'}
    0x091C: {id: f_po512, doc: 'Giant ''Winnie the Pooh'' book (with illustration)'}
    0x091D: {id: f_hb650_01, doc: '''Winnie the Pooh'' book (torn cover, dummy)'}
    0x091E: {id: n_ex500_anger_raw_rtn, doc: 'Heyner (dummy)'}
    0x091F: {id: n_lk020_gm, doc: 'Pumba (dummy, freezes instantly)'}
    0x0920: {id: b_mu100_gm, doc: 'Shan Yu (Dummy, Freezes instantly)'}
    0x0921: {id: b_tr000_gm, doc: 'Hostile Program (Dummy, Freezes instantly)'}
    0x0922: {id: m_ex990_rtn_fixcolor, doc: 'Dusk (doesn''t move)'}
    0x0923: {id: n_cm000_btl, doc: 'Marluxia (Absent Silhouette)'}
    0x0924: {id: h_ex660_ev, doc: 'Ansem-Riku in cloak, no hood (dummy)'}
    0x0925: {id: n_ex840_ev, doc: 'Org XIII member, no face. (dummy)'}
    0x0926: {id: m_ex350_01, doc: 'Mushroom 1'}
    0x0927: {id: m_ex350_02, doc: 'Mushroom 2'}
    0x0928: {id: m_ex350_03, doc: 'Mushroom 3'}
    0x0929: {id: m_ex350_04, doc: 'Mushroom 4'}
    0x092A: {id: m_ex350_05, doc: 'Mushroom 5'}
    0x092B: {id: m_ex350_06, doc: 'Mushroom 6'}
    0x092C: {id: m_ex350_07, doc: 'Mushroom 7'}
    0x092D: {id: m_ex350_08, doc: 'Mushroom 8'}
    0x092E: {id: m_ex350_09, doc: 'Mushroom 9'}
    0x092F: {id: m_ex350_10, doc: 'Mushroom 10'}
    0x0930: {id: m_ex350_11, doc: 'Mushroom 11 (Excellent way to gain rare items easily)'}
    0x0931: {id: m_ex350_12, doc: 'Mushroom 12'}
    0x0932: {id: m_ex350_13, doc: 'Nothing'}
    0x0933: {id: n_cm040_btl, doc: 'Vexen (Absent Silhouette)'}
    0x0934: {id: p_ex100_htlf_btl, doc: 'Vexen''s Anti-Sora'}
    0x0935: {id: n_cm020_btl, doc: 'Lexeus (Absent Silhouette)'}
    0x0936: {id: n_ex820_btl, doc: 'Cloaked duel weilding Roxas (Attackable dummy)'}
    0x0937: {id: n_cm010_btl, doc: 'Larxene (Absent Silhouette, doesn''t fight back, and also can''t be killed)'}
    0x0938: {id: n_cm030_btl, doc: 'Zexion (Absent Silhouette, attackable dummy)'}
    0x0939: {id: w_ex010_p0, doc: 'A Crossed Two keyblade (dummy)'}
    0x093A: {id: w_ex010_r0, doc: '13 Mushrooms keyblade (dummy)'}
    0x093B: {id: w_ex010_p0_nm, doc: 'Nothing '}
    0x093C: {id: w_ex010_r0_nm, doc: 'Nothing'}
    0x093D: {id: w_ex010_p0_tr, doc: 'Nothing'}
    0x093E: {id: w_ex010_r0_tr, doc: 'Nothing'}
    0x093F: {id: w_ex010_p0_wi, doc: 'Nothing '}
    0x0940: {id: w_ex010_r0_wi, doc: 'Nothing '}
    0x0941: {id: w_ex020_b0, doc: 'Lexeus'' weapon'}
    0x0942: {id: w_ex020_c0, doc: 'Mushroom staff weapon'}
    0x0943: {id: w_ex020_b0_nm, doc: 'Nothing '}
    0x0944: {id: w_ex020_c0_nm, doc: 'Nothing'}
    0x0945: {id: w_ex020_b0_tr, doc: 'Nothing'}
    0x0946: {id: w_ex020_c0_tr, doc: 'Nothing'}
    0x0947: {id: w_ex020_b0_wi, doc: 'Nothing'}
    0x0948: {id: w_ex020_c0_wi, doc: 'Nothing'}
    0x0949: {id: w_ex030_b0, doc: 'Goofy''s Vexen weapon (dummy)'}
    0x094A: {id: w_ex030_c0, doc: 'Mushroom shield (dummy)'}
    0x094B: {id: w_ex030_b0_nm, doc: 'Nothing'}
    0x094C: {id: w_ex030_c0_nm, doc: 'Nothing'}
    0x094D: {id: w_ex030_b0_tr, doc: 'Nothing'}
    0x094E: {id: w_ex030_c0_tr, doc: 'Nothing'}
    0x094F: {id: w_ex030_b0_wi, doc: 'Nothing'}
    0x0950: {id: w_ex030_c0_wi, doc: 'Nothing'}
    0x0951: {id: b_ex390, doc: 'Hooded Roxas (dual weiding enemy)'}
    0x0952: {id: w_ex010_roxas_light, doc: 'Nothing'}
    0x0953: {id: w_ex010_roxas_dark, doc: 'Nothing'}
    0x0954: {id: f_hb090, doc: 'Whirlwind (jumpable)'}
    0x0955: {id: p_ex100_xm, doc: 'Sora (Christmas town form.... weapon IS Donald)'}
    0x0956: {id: p_ex100_xm_btlf, doc: 'Freeze'}
    0x0957: {id: p_ex100_xm_magf, doc: 'Freeze'}
    0x0958: {id: p_ex100_xm_trif, doc: 'Freeze'}
    0x0959: {id: p_ex100_xm_ultf, doc: 'Freeze'}
    0x095A: {id: p_ex100_xm_htlf, doc: 'Anti-Sora (Christmas Town form)'}
    0x095B: {id: p_ex020_xm, doc: 'Donald (Christmas Town form, freezes if you wander too far)'}
    0x095C: {id: p_ex030_xm, doc: 'Goofy (Christmas Town form, freezes if you wander too far)'}
    0x095D: {id: p_ex100_kh1f, doc: 'Limit form'}
    0x095E: {id: p_ex100_nm_kh1f, doc: 'freeze'}
    0x095F: {id: p_ex100_xm_kh1f, doc: 'freeze'}
    0x0960: {id: p_ex100_tr_kh1f, doc: 'freeze'}
    0x0961: {id: p_ex100_wi_kh1f, doc: 'freeze'}
    0x0962: {id: b_ex400, doc: 'Larxene (Absent Silhouette)'}
    0x0963: {id: m_ex500_hb, doc: 'Magic Phantom'}
    0x0964: {id: m_ex520_hb, doc: 'Perplex'}
    0x0965: {id: m_ex560_hb, doc: 'Iron Hammer'}
    0x0966: {id: m_ex640_hb, doc: 'Mad Bumper'}
    0x0967: {id: m_ex650_hb, doc: 'Silent Launcher'}
    0x0968: {id: m_ex680_hb, doc: 'Reckless'}
    0x0969: {id: m_ex690_hb, doc: 'Lance Warrior'}
    0x096A: {id: m_ex780_hb, doc: 'Aeriel Champ'}
    0x096B: {id: m_ex720_hb, doc: 'Necromancer'}
    0x096C: {id: m_ex120_hb, doc: 'Spring Metal'}
    0x096D: {id: m_ex210_hb, doc: 'Air Viking'}
    0x096E: {id: m_ex530_hb, doc: 'Rune Master'}
    0x096F: {id: b_ex420, doc: 'TERRA!!!'}
    0x0970: {id: f_hb050, doc: 'Pushing pillar from cave of remembrance'}
    0x0971: {id: f_hb060, doc: 'Rising pillar from cave of remembrance'}
    0x0972: {id: f_ex210, doc: 'Vexen''s Absent Silhouette portal (can''t be used)'}
    0x0973: {id: f_ex220, doc: 'Lexeus'' Absent Silhouette portal (can''t be used)'}
    0x0974: {id: f_ex230, doc: 'Zexion''s Absent Silhouette portal (can''t be used)'}
    0x0975: {id: f_ex240, doc: 'Marluxia''s Absent Silhouette portal (can''t be used)'}
    0x0976: {id: f_ex250, doc: 'Larxene''s Absent Silhouette portal (can''t be used)'}
    0x0977: {id: n_zz160_ev, doc: 'KH1 Ansem with Heartless guardian (dummy)'}
    0x0978: {id: f_hb740, doc: 'Spiral pillar from Cave of Remebmrance'}
    0x0979: {id: f_hb750, doc: 'Pillar on it''s side from Cave of Remembrance (not solid)'}
    0x097A: {id: f_hb760, doc: 'Pillar from Cave of Remembrance (not solid)'}
    0x097B: {id: b_ex370, doc: 'Zexion (Absent Silhouette)'}
    0x097C: {id: m_ex500_hb_gm, doc: 'Freeze'}
    0x097D: {id: b_ex380, doc: 'Zexion''s book'}
    0x097E: {id: b_ex410, doc: 'Nothing (?)'}
    0x097F: {id: h_ex500_kh1f, doc: 'Limit form (dummy)'}
    0x0980: {id: h_ex500_nm_kh1f, doc: 'Limit form (Halloween town, dummy)'}
    0x0981: {id: h_ex500_xm, doc: 'Sora (Christmas town, dummy)'}
    0x0982: {id: h_ex500_xm_btlf, doc: 'Sora (Christmas town, Valor Form, dummy)'}
    0x0983: {id: h_ex500_xm_magf, doc: 'Sora (Christmas town, Wisdom Form, dummy)'}
    0x0984: {id: h_ex500_xm_trif, doc: 'Sora (Christmas town, Limit Form, dummy)'}
    0x0985: {id: h_ex500_xm_ultf, doc: 'Sora (Christmas town, Master Form, dummy)'}
    0x0986: {id: h_ex500_xm_kh1f, doc: 'Sora (Christmas town, Final Form, dummy)'}
    0x0987: {id: h_ex500_tr_kh1f, doc: 'Sora (Space Paranoids form, Limit Form, dummy)'}
    0x0988: {id: f_ex260, doc: 'Puzzle crown'}
    0x0989: {id: w_ex020_a4, doc: 'Save The Queen (Re-colour, dummy)'}
    0x098A: {id: w_ex020_b4, doc: 'Donalds Lexeus weapon (Re-colour, dummy)'}
    0x098B: {id: w_ex020_c4, doc: 'Mushroom wand (dummy)'}
    0x098C: {id: w_ex020_c8, doc: 'Mushroom wand (dummy)'}
    0x098D: {id: w_ex020_cc, doc: 'Mushroom wand (dummy)'}
    0x098E: {id: w_ex020_cg, doc: 'Mushroom wand (dummy)'}
    0x098F: {id: w_ex020_a4_nm, doc: 'Nothing'}
    0x0990: {id: w_ex020_b4_nm, doc: 'Nothing'}
    0x0991: {id: w_ex020_c4_nm, doc: 'Nothing'}
    0x0992: {id: w_ex020_c8_nm, doc: 'Nothing'}
    0x0993: {id: w_ex020_cc_nm, doc: 'Nothing'}
    0x0994: {id: w_ex020_cg_nm, doc: 'Nothing'}
    0x0995: {id: w_ex020_a4_tr, doc: ''}
    0x0996: {id: w_ex020_b4_tr, doc: ''}
    0x0997: {id: w_ex020_c4_tr, doc: ''}
    0x0998: {id: w_ex020_c8_tr, doc: 'Nothing'}
    0x0999: {id: w_ex020_cc_tr, doc: 'Freeze'}
    0x099A: {id: w_ex020_cg_tr, doc: 'Nothing'}
    0x099B: {id: w_ex020_a4_wi, doc: ''}
    0x099C: {id: w_ex020_b4_wi, doc: ''}
    0x099D: {id: w_ex020_c4_wi, doc: ''}
    0x099E: {id: w_ex020_c8_wi, doc: ''}
    0x099F: {id: w_ex020_cc_wi, doc: 'Nothing'}
    0x09A0: {id: w_ex020_cg_wi, doc: 'Nothing'}
    0x09A1: {id: w_ex030_a4, doc: 'Save the King+'}
    0x09A2: {id: w_ex030_b4, doc: 'Freeze Pride+'}
    0x09A3: {id: w_ex030_c4, doc: 'Mushroom shield'}
    0x09A4: {id: w_ex030_c8, doc: 'Mushroom shield'}
    0x09A5: {id: w_ex030_cc, doc: 'Mushroom shield'}
    0x09A6: {id: w_ex030_cg, doc: 'Mushroom shield '}
    0x09A7: {id: w_ex030_a4_nm, doc: 'Nothing'}
    0x09A8: {id: w_ex030_b4_nm, doc: 'Nothing'}
    0x09A9: {id: w_ex030_c4_nm, doc: 'Nothing'}
    0x09AA: {id: w_ex030_c8_nm, doc: 'Nothing'}
    0x09AB: {id: w_ex030_cc_nm, doc: 'Nothing'}
    0x09AC: {id: w_ex030_cg_nm, doc: 'Nothing'}
    0x09AD: {id: w_ex030_a4_tr, doc: 'Nothing'}
    0x09AE: {id: w_ex030_b4_tr, doc: 'Nothing'}
    0x09AF: {id: w_ex030_c4_tr, doc: 'Nothing'}
    0x09B0: {id: w_ex030_c8_tr, doc: 'Nothing'}
    0x09B1: {id: w_ex030_cc_tr, doc: 'Nothing'}
    0x09B2: {id: w_ex030_cg_tr, doc: 'Nothing'}
    0x09B3: {id: w_ex030_a4_wi, doc: 'Nothing'}
    0x09B4: {id: w_ex030_b4_wi, doc: 'Nothing'}
    0x09B5: {id: w_ex030_c4_wi, doc: 'Nothing'}
    0x09B6: {id: w_ex030_c8_wi, doc: 'Nothing'}
    0x09B7: {id: w_ex030_cc_wi, doc: 'Nothing'}
    0x09B8: {id: w_ex030_cg_wi, doc: 'Nothing'}
    0x09B9: {id: f_hb140, doc: 'Cavern of Remembrance big pushing block'}
    0x09BA: {id: f_hb040, doc: 'Cavern of Remembrance red orb'}
    0x09BB: {id: f_hb050_23, doc: 'CoR small pushing block'}
    0x09BC: {id: f_hb040_bl, doc: 'CoR blue orb'}
    0x09BD: {id: f_hb040_ye, doc: 'CoR yellow orb'}
    0x09BE: {id: f_hb040_wh, doc: 'CoR white orb'}
    0x09BF: {id: f_hb070, doc: 'CoR stone slab (invisible, has HP)'}
    0x09C0: {id: f_hb080, doc: 'CoR steam wheel (invisible, has HP)'}
    0x09C1: {id: f_hb110, doc: 'Nothing'}
    0x09C2: {id: f_hb120, doc: 'Nothing'}
    0x09C3: {id: f_hb130, doc: 'Nothing'}
    0x09C4: {id: b_ex110_lv99, doc: 'Axel (Limit Cut)'}
    0x09C5: {id: b_ex140_lv99, doc: 'Xigbar (Limit Cut)'}
    0x09C6: {id: b_ex160_lv99, doc: 'Saïx (Limit Cut)'}
    0x09C7: {id: f_hb100, doc: 'Nothing'}
    0x09C8: {id: b_ex150_lv99, doc: 'Luxord (Limit Cut)'}
    0x09C9: {id: b_ex170_lv99, doc: 'Xemnas (Limit Cut Memory''s Contortion)'}
    0x09CA: {id: b_ex170_last_lv99, doc: 'Xemnas (Limit Cut The World of Nothing)?'}
    0x09CB: {id: b_ex130_lv99, doc: 'Xaldin (Limit Cut)'}
    0x09CC: {id: b_ex120_hb_lv99, doc: 'Demyx (Limit Cut)'}
    0x09CD: {id: m_ex590_nm, doc: 'Bulky Vendor (Halloween Town)'}
    0x09CE: {id: f_hb730, doc: 'Garden of Assemblage door'}
    0x09CF: {id: w_ex030_b0_lk, doc: 'Freeze Pride'}
    0x09D0: {id: w_ex030_b4_lk, doc: 'Freeze Pride+'}
    0x09D1: {id: prize_m_ex350_03, doc: 'Nothing'}
    0x09E5: {id: f_hb710, doc: ''}
    0x09E6: {id: f_hb720, doc: ''}
    0x09E7: {id: n_ex970, doc: ''}
    0x09E8: {id: f_hb770, doc: ''}
    0x09E9: {id: f_hb780, doc: ''}
    0x09EA: {id: f_hb790, doc: ''}
    0x09EB: {id: m_ex350_06_su, doc: ''}
    0x09EC: {id: f_ex040_hb, doc: ''}
    0x09ED: {id: p_ex020_xm_rtn, doc: ''}
    0x09EE: {id: p_ex030_xm_rtn, doc: ''}
    0x09EF: {id: n_ex700_tt_spo_raw_talk_rtn, doc: ''}
    0x09F0: {id: n_ex700_tt_spo_raw2_rtn, doc: ''}
    0x09F1: {id: n_ex700_tt_spo_rw2_rtn, doc: ''}
    0x09F2: {id: w_ex020_94, doc: ''}
    0x09F3: {id: w_ex020_94_nm, doc: ''}
    0x09F4: {id: w_ex020_94_tr, doc: ''}
    0x09F5: {id: w_ex020_94_wi, doc: ''}
    0x09F6: {id: w_ex030_84, doc: ''}
    0x09F7: {id: w_ex030_84_nm, doc: ''}
    0x09F8: {id: w_ex030_84_tr, doc: ''}
    0x09F9: {id: w_ex030_84_wi, doc: ''}
    0x09FA: {id: m_ex880_dancer_lv99, doc: ''}
    0x09FB: {id: f_ex000_dc, doc: ''}
    0x09FC: {id: b_ex220_lv99, doc: ''}
    0x09FD: {id: b_ex430, doc: ''}
    0x09FE: {id: m_ex350_02_gm, doc: ''}
    0x09FF: {id: m_ex350_11_gm, doc: ''}
    0x0A00: {id: f_hb601, doc: ''}
    0x0A01: {id: h_cm030, doc: ''}
