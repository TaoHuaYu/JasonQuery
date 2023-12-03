WITH mm AS (SELECT vm.* FROM  view_map vm 
             WHERE vm.rcno='A9013867' 
               AND (vm.step = 'CP1' or vm.step = 'CB1') 
               AND vm.wfid = 23
           ), 
     dd AS (SELECT 
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 1), 0) AS D01,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 2), 0) AS D02,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 3), 0) AS D03,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 4), 0) AS D04,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 5), 0) AS D05,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 6), 0) AS D06,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 7), 0) AS D07,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 8), 0) AS D08,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 9), 0) AS D09,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 10), 0) AS D10,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 11), 0) AS D11,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 12), 0) AS D12,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 13), 0) AS D13,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 14), 0) AS D14,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 15), 0) AS D15,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 16), 0) AS D16,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 17), 0) AS D17,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 18), 0) AS D18,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 19), 0) AS D19,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 20), 0) AS D20,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 21), 0) AS D21,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 22), 0) AS D22,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 23), 0) AS D23,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 24), 0) AS D24,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 25), 0) AS D25,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 26), 0) AS D26,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 27), 0) AS D27,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 28), 0) AS D28,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 29), 0) AS D29,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 30), 0) AS D30,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 31), 0) AS D31,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 32), 0) AS D32,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 33), 0) AS D33,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 34), 0) AS D34,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 35), 0) AS D35,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 36), 0) AS D36,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 37), 0) AS D37,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 38), 0) AS D38,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 39), 0) AS D39,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 40), 0) AS D40,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 41), 0) AS D41,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 42), 0) AS D42,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 43), 0) AS D43,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 44), 0) AS D44,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 45), 0) AS D45,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 46), 0) AS D46,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 47), 0) AS D47,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 48), 0) AS D48,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 49), 0) AS D49,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 50), 0) AS D50,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 51), 0) AS D51,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 52), 0) AS D52,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 53), 0) AS D53,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 54), 0) AS D54,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 55), 0) AS D55,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 56), 0) AS D56,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 57), 0) AS D57,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 58), 0) AS D58,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 59), 0) AS D59,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 60), 0) AS D60,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 61), 0) AS D61,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 62), 0) AS D62,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 63), 0) AS D63,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 64), 0) AS D64,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 65), 0) AS D65,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 66), 0) AS D66,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 67), 0) AS D67,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 68), 0) AS D68,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 69), 0) AS D69,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 70), 0) AS D70,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 71), 0) AS D71,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 72), 0) AS D72,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 73), 0) AS D73,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 74), 0) AS D74,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 75), 0) AS D75,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 76), 0) AS D76,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 77), 0) AS D77,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 78), 0) AS D78,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 79), 0) AS D79,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 80), 0) AS D80,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 81), 0) AS D81,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 82), 0) AS D82,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 83), 0) AS D83,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 84), 0) AS D84,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 85), 0) AS D85,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 86), 0) AS D86,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 87), 0) AS D87,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 88), 0) AS D88,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 89), 0) AS D89,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 90), 0) AS D90,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 91), 0) AS D91,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 92), 0) AS D92,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 93), 0) AS D93,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 94), 0) AS D94,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 95), 0) AS D95,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 96), 0) AS D96,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 97), 0) AS D97,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 98), 0) AS D98,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 99), 0) AS D99,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 100), 0) AS D100,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 101), 0) AS D101,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 102), 0) AS D102,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 103), 0) AS D103,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 104), 0) AS D104,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 105), 0) AS D105,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 106), 0) AS D106,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 107), 0) AS D107,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 108), 0) AS D108,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 109), 0) AS D109,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 110), 0) AS D110,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 111), 0) AS D111,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 112), 0) AS D112,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 113), 0) AS D113,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 114), 0) AS D114,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 115), 0) AS D115,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 116), 0) AS D116,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 117), 0) AS D117,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 118), 0) AS D118,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 119), 0) AS D119,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 120), 0) AS D120,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 121), 0) AS D121,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 122), 0) AS D122,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 123), 0) AS D123,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 124), 0) AS D124,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 125), 0) AS D125,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 126), 0) AS D126,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 127), 0) AS D127,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 128), 0) AS D128,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 129), 0) AS D129,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 130), 0) AS D130,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 131), 0) AS D131,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 132), 0) AS D132,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 133), 0) AS D133,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 134), 0) AS D134,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 135), 0) AS D135,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 136), 0) AS D136,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 137), 0) AS D137,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 138), 0) AS D138,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 139), 0) AS D139,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 140), 0) AS D140,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 141), 0) AS D141,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 142), 0) AS D142,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 143), 0) AS D143,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 144), 0) AS D144,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 145), 0) AS D145,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 146), 0) AS D146,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 147), 0) AS D147,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 148), 0) AS D148,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 149), 0) AS D149,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 150), 0) AS D150,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 151), 0) AS D151,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 152), 0) AS D152,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 153), 0) AS D153,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 154), 0) AS D154,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 155), 0) AS D155,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 156), 0) AS D156,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 157), 0) AS D157,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 158), 0) AS D158,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 159), 0) AS D159,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 160), 0) AS D160,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 161), 0) AS D161,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 162), 0) AS D162,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 163), 0) AS D163,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 164), 0) AS D164,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 165), 0) AS D165,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 166), 0) AS D166,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 167), 0) AS D167,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 168), 0) AS D168,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 169), 0) AS D169,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 170), 0) AS D170,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 171), 0) AS D171,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 172), 0) AS D172,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 173), 0) AS D173,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 174), 0) AS D174,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 175), 0) AS D175,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 176), 0) AS D176,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 177), 0) AS D177,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 178), 0) AS D178,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 179), 0) AS D179,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 180), 0) AS D180,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 181), 0) AS D181,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 182), 0) AS D182,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 183), 0) AS D183,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 184), 0) AS D184,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 185), 0) AS D185,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 186), 0) AS D186,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 187), 0) AS D187,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 188), 0) AS D188,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 189), 0) AS D189,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 190), 0) AS D190,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 191), 0) AS D191,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 192), 0) AS D192,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 193), 0) AS D193,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 194), 0) AS D194,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 195), 0) AS D195,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 196), 0) AS D196,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 197), 0) AS D197,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 198), 0) AS D198,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 199), 0) AS D199,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 200), 0) AS D200,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 201), 0) AS D201,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 202), 0) AS D202,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 203), 0) AS D203,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 204), 0) AS D204,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 205), 0) AS D205,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 206), 0) AS D206,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 207), 0) AS D207,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 208), 0) AS D208,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 209), 0) AS D209,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 210), 0) AS D210,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 211), 0) AS D211,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 212), 0) AS D212,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 213), 0) AS D213,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 214), 0) AS D214,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 215), 0) AS D215,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 216), 0) AS D216,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 217), 0) AS D217,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 218), 0) AS D218,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 219), 0) AS D219,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 220), 0) AS D220,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 221), 0) AS D221,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 222), 0) AS D222,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 223), 0) AS D223,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 224), 0) AS D224,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 225), 0) AS D225,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 226), 0) AS D226,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 227), 0) AS D227,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 228), 0) AS D228,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 229), 0) AS D229,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 230), 0) AS D230,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 231), 0) AS D231,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 232), 0) AS D232,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 233), 0) AS D233,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 234), 0) AS D234,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 235), 0) AS D235,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 236), 0) AS D236,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 237), 0) AS D237,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 238), 0) AS D238,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 239), 0) AS D239,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 240), 0) AS D240,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 241), 0) AS D241,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 242), 0) AS D242,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 243), 0) AS D243,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 244), 0) AS D244,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 245), 0) AS D245,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 246), 0) AS D246,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 247), 0) AS D247,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 248), 0) AS D248,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 249), 0) AS D249,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 250), 0) AS D250,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 251), 0) AS D251,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 252), 0) AS D252,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 253), 0) AS D253,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 254), 0) AS D254,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 255), 0) AS D255,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 256), 0) AS D256,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 257), 0) AS D257,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 258), 0) AS D258,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 259), 0) AS D259,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 260), 0) AS D260,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 261), 0) AS D261,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 262), 0) AS D262,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 263), 0) AS D263,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 264), 0) AS D264,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 265), 0) AS D265,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 266), 0) AS D266,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 267), 0) AS D267,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 268), 0) AS D268,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 269), 0) AS D269,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 270), 0) AS D270,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 271), 0) AS D271,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 272), 0) AS D272,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 273), 0) AS D273,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 274), 0) AS D274,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 275), 0) AS D275,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 276), 0) AS D276,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 277), 0) AS D277,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 278), 0) AS D278,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 279), 0) AS D279,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 280), 0) AS D280,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 281), 0) AS D281,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 282), 0) AS D282,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 283), 0) AS D283,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 284), 0) AS D284,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 285), 0) AS D285,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 286), 0) AS D286,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 287), 0) AS D287,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 288), 0) AS D288,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 289), 0) AS D289,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 290), 0) AS D290,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 291), 0) AS D291,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 292), 0) AS D292,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 293), 0) AS D293,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 294), 0) AS D294,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 295), 0) AS D295,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 296), 0) AS D296,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 297), 0) AS D297,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 298), 0) AS D298,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 299), 0) AS D299,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 300), 0) AS D300,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 301), 0) AS D301,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 302), 0) AS D302,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 303), 0) AS D303,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 304), 0) AS D304,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 305), 0) AS D305,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 306), 0) AS D306,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 307), 0) AS D307,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 308), 0) AS D308,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 309), 0) AS D309,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 310), 0) AS D310,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 311), 0) AS D311,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 312), 0) AS D312,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 313), 0) AS D313,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 314), 0) AS D314,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 315), 0) AS D315,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 316), 0) AS D316,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 317), 0) AS D317,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 318), 0) AS D318,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 319), 0) AS D319,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 320), 0) AS D320,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 321), 0) AS D321,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 322), 0) AS D322,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 323), 0) AS D323,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 324), 0) AS D324,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 325), 0) AS D325,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 326), 0) AS D326,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 327), 0) AS D327,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 328), 0) AS D328,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 329), 0) AS D329,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 330), 0) AS D330,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 331), 0) AS D331,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 332), 0) AS D332,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 333), 0) AS D333,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 334), 0) AS D334,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 335), 0) AS D335,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 336), 0) AS D336,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 337), 0) AS D337,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 338), 0) AS D338,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 339), 0) AS D339,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 340), 0) AS D340,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 341), 0) AS D341,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 342), 0) AS D342,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 343), 0) AS D343,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 344), 0) AS D344,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 345), 0) AS D345,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 346), 0) AS D346,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 347), 0) AS D347,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 348), 0) AS D348,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 349), 0) AS D349,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 350), 0) AS D350,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 351), 0) AS D351,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 352), 0) AS D352,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 353), 0) AS D353,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 354), 0) AS D354,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 355), 0) AS D355,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 356), 0) AS D356,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 357), 0) AS D357,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 358), 0) AS D358,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 359), 0) AS D359,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 360), 0) AS D360,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 361), 0) AS D361,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 362), 0) AS D362,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 363), 0) AS D363,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 364), 0) AS D364,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 365), 0) AS D365,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 366), 0) AS D366,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 367), 0) AS D367,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 368), 0) AS D368,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 369), 0) AS D369,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 370), 0) AS D370,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 371), 0) AS D371,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 372), 0) AS D372,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 373), 0) AS D373,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 374), 0) AS D374,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 375), 0) AS D375,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 376), 0) AS D376,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 377), 0) AS D377,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 378), 0) AS D378,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 379), 0) AS D379,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 380), 0) AS D380,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 381), 0) AS D381,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 382), 0) AS D382,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 383), 0) AS D383,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 384), 0) AS D384,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 385), 0) AS D385,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 386), 0) AS D386,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 387), 0) AS D387,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 388), 0) AS D388,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 389), 0) AS D389,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 390), 0) AS D390,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 391), 0) AS D391,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 392), 0) AS D392,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 393), 0) AS D393,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 394), 0) AS D394,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 395), 0) AS D395,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 396), 0) AS D396,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 397), 0) AS D397,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 398), 0) AS D398,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 399), 0) AS D399,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 400), 0) AS D400,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 401), 0) AS D401,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 402), 0) AS D402,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 403), 0) AS D403,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 404), 0) AS D404,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 405), 0) AS D405,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 406), 0) AS D406,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 407), 0) AS D407,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 408), 0) AS D408,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 409), 0) AS D409,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 410), 0) AS D410,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 411), 0) AS D411,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 412), 0) AS D412,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 413), 0) AS D413,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 414), 0) AS D414,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 415), 0) AS D415,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 416), 0) AS D416,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 417), 0) AS D417,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 418), 0) AS D418,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 419), 0) AS D419,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 420), 0) AS D420,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 421), 0) AS D421,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 422), 0) AS D422,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 423), 0) AS D423,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 424), 0) AS D424,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 425), 0) AS D425,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 426), 0) AS D426,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 427), 0) AS D427,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 428), 0) AS D428,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 429), 0) AS D429,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 430), 0) AS D430,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 431), 0) AS D431,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 432), 0) AS D432,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 433), 0) AS D433,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 434), 0) AS D434,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 435), 0) AS D435,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 436), 0) AS D436,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 437), 0) AS D437,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 438), 0) AS D438,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 439), 0) AS D439,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 440), 0) AS D440,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 441), 0) AS D441,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 442), 0) AS D442,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 443), 0) AS D443,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 444), 0) AS D444,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 445), 0) AS D445,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 446), 0) AS D446,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 447), 0) AS D447,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 448), 0) AS D448,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 449), 0) AS D449,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 450), 0) AS D450,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 451), 0) AS D451,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 452), 0) AS D452,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 453), 0) AS D453,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 454), 0) AS D454,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 455), 0) AS D455,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 456), 0) AS D456,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 457), 0) AS D457,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 458), 0) AS D458,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 459), 0) AS D459,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 460), 0) AS D460,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 461), 0) AS D461,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 462), 0) AS D462,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 463), 0) AS D463,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 464), 0) AS D464,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 465), 0) AS D465,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 466), 0) AS D466,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 467), 0) AS D467,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 468), 0) AS D468,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 469), 0) AS D469,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 470), 0) AS D470,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 471), 0) AS D471,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 472), 0) AS D472,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 473), 0) AS D473,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 474), 0) AS D474,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 475), 0) AS D475,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 476), 0) AS D476,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 477), 0) AS D477,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 478), 0) AS D478,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 479), 0) AS D479,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 480), 0) AS D480,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 481), 0) AS D481,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 482), 0) AS D482,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 483), 0) AS D483,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 484), 0) AS D484,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 485), 0) AS D485,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 486), 0) AS D486,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 487), 0) AS D487,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 488), 0) AS D488,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 489), 0) AS D489,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 490), 0) AS D490,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 491), 0) AS D491,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 492), 0) AS D492,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 493), 0) AS D493,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 494), 0) AS D494,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 495), 0) AS D495,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 496), 0) AS D496,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 497), 0) AS D497,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 498), 0) AS D498,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 499), 0) AS D499,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 500), 0) AS D500,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 501), 0) AS D501,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 502), 0) AS D502,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 503), 0) AS D503,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 504), 0) AS D504,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 505), 0) AS D505,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 506), 0) AS D506,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 507), 0) AS D507,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 508), 0) AS D508,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 509), 0) AS D509,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 510), 0) AS D510,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 511), 0) AS D511,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 512), 0) AS D512,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 513), 0) AS D513,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 514), 0) AS D514,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 515), 0) AS D515,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 516), 0) AS D516,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 517), 0) AS D517,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 518), 0) AS D518,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 519), 0) AS D519,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 520), 0) AS D520,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 521), 0) AS D521,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 522), 0) AS D522,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 523), 0) AS D523,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 524), 0) AS D524,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 525), 0) AS D525,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 526), 0) AS D526,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 527), 0) AS D527,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 528), 0) AS D528,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 529), 0) AS D529,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 530), 0) AS D530,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 531), 0) AS D531,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 532), 0) AS D532,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 533), 0) AS D533,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 534), 0) AS D534,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 535), 0) AS D535,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 536), 0) AS D536,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 537), 0) AS D537,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 538), 0) AS D538,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 539), 0) AS D539,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 540), 0) AS D540,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 541), 0) AS D541,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 542), 0) AS D542,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 543), 0) AS D543,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 544), 0) AS D544,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 545), 0) AS D545,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 546), 0) AS D546,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 547), 0) AS D547,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 548), 0) AS D548,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 549), 0) AS D549,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 550), 0) AS D550,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 551), 0) AS D551,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 552), 0) AS D552,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 553), 0) AS D553,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 554), 0) AS D554,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 555), 0) AS D555,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 556), 0) AS D556,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 557), 0) AS D557,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 558), 0) AS D558,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 559), 0) AS D559,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 560), 0) AS D560,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 561), 0) AS D561,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 562), 0) AS D562,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 563), 0) AS D563,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 564), 0) AS D564,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 565), 0) AS D565,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 566), 0) AS D566,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 567), 0) AS D567,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 568), 0) AS D568,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 569), 0) AS D569,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 570), 0) AS D570,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 571), 0) AS D571,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 572), 0) AS D572,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 573), 0) AS D573,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 574), 0) AS D574,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 575), 0) AS D575,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 576), 0) AS D576,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 577), 0) AS D577,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 578), 0) AS D578,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 579), 0) AS D579,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 580), 0) AS D580,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 581), 0) AS D581,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 582), 0) AS D582,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 583), 0) AS D583,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 584), 0) AS D584,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 585), 0) AS D585,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 586), 0) AS D586,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 587), 0) AS D587,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 588), 0) AS D588,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 589), 0) AS D589,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 590), 0) AS D590,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 591), 0) AS D591,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 592), 0) AS D592,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 593), 0) AS D593,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 594), 0) AS D594,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 595), 0) AS D595,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 596), 0) AS D596,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 597), 0) AS D597,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 598), 0) AS D598,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 599), 0) AS D599,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 600), 0) AS D600,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 601), 0) AS D601,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 602), 0) AS D602,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 603), 0) AS D603,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 604), 0) AS D604,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 605), 0) AS D605,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 606), 0) AS D606,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 607), 0) AS D607,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 608), 0) AS D608,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 609), 0) AS D609,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 610), 0) AS D610,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 611), 0) AS D611,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 612), 0) AS D612,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 613), 0) AS D613,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 614), 0) AS D614,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 615), 0) AS D615,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 616), 0) AS D616,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 617), 0) AS D617,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 618), 0) AS D618,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 619), 0) AS D619,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 620), 0) AS D620,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 621), 0) AS D621,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 622), 0) AS D622,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 623), 0) AS D623,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 624), 0) AS D624,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 625), 0) AS D625,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 626), 0) AS D626,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 627), 0) AS D627,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 628), 0) AS D628,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 629), 0) AS D629,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 630), 0) AS D630,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 631), 0) AS D631,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 632), 0) AS D632,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 633), 0) AS D633,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 634), 0) AS D634,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 635), 0) AS D635,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 636), 0) AS D636,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 637), 0) AS D637,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 638), 0) AS D638,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 639), 0) AS D639,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 640), 0) AS D640,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 641), 0) AS D641,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 642), 0) AS D642,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 643), 0) AS D643,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 644), 0) AS D644,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 645), 0) AS D645,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 646), 0) AS D646,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 647), 0) AS D647,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 648), 0) AS D648,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 649), 0) AS D649,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 650), 0) AS D650,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 651), 0) AS D651,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 652), 0) AS D652,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 653), 0) AS D653,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 654), 0) AS D654,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 655), 0) AS D655,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 656), 0) AS D656,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 657), 0) AS D657,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 658), 0) AS D658,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 659), 0) AS D659,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 660), 0) AS D660,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 661), 0) AS D661,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 662), 0) AS D662,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 663), 0) AS D663,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 664), 0) AS D664,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 665), 0) AS D665,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 666), 0) AS D666,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 667), 0) AS D667,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 668), 0) AS D668,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 669), 0) AS D669,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 670), 0) AS D670,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 671), 0) AS D671,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 672), 0) AS D672,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 673), 0) AS D673,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 674), 0) AS D674,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 675), 0) AS D675,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 676), 0) AS D676,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 677), 0) AS D677,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 678), 0) AS D678,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 679), 0) AS D679,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 680), 0) AS D680,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 681), 0) AS D681,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 682), 0) AS D682,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 683), 0) AS D683,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 684), 0) AS D684,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 685), 0) AS D685,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 686), 0) AS D686,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 687), 0) AS D687,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 688), 0) AS D688,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 689), 0) AS D689,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 690), 0) AS D690,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 691), 0) AS D691,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 692), 0) AS D692,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 693), 0) AS D693,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 694), 0) AS D694,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 695), 0) AS D695,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 696), 0) AS D696,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 697), 0) AS D697,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 698), 0) AS D698,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 699), 0) AS D699,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 700), 0) AS D700,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 701), 0) AS D701,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 702), 0) AS D702,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 703), 0) AS D703,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 704), 0) AS D704,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 705), 0) AS D705,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 706), 0) AS D706,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 707), 0) AS D707,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 708), 0) AS D708,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 709), 0) AS D709,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 710), 0) AS D710,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 711), 0) AS D711,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 712), 0) AS D712,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 713), 0) AS D713,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 714), 0) AS D714,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 715), 0) AS D715,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 716), 0) AS D716,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 717), 0) AS D717,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 718), 0) AS D718,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 719), 0) AS D719,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 720), 0) AS D720,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 721), 0) AS D721,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 722), 0) AS D722,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 723), 0) AS D723,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 724), 0) AS D724,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 725), 0) AS D725,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 726), 0) AS D726,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 727), 0) AS D727,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 728), 0) AS D728,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 729), 0) AS D729,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 730), 0) AS D730,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 731), 0) AS D731,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 732), 0) AS D732,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 733), 0) AS D733,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 734), 0) AS D734,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 735), 0) AS D735,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 736), 0) AS D736,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 737), 0) AS D737,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 738), 0) AS D738,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 739), 0) AS D739,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 740), 0) AS D740,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 741), 0) AS D741,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 742), 0) AS D742,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 743), 0) AS D743,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 744), 0) AS D744,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 745), 0) AS D745,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 746), 0) AS D746,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 747), 0) AS D747,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 748), 0) AS D748,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 749), 0) AS D749,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 750), 0) AS D750,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 751), 0) AS D751,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 752), 0) AS D752,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 753), 0) AS D753,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 754), 0) AS D754,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 755), 0) AS D755,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 756), 0) AS D756,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 757), 0) AS D757,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 758), 0) AS D758,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 759), 0) AS D759,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 760), 0) AS D760,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 761), 0) AS D761,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 762), 0) AS D762,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 763), 0) AS D763,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 764), 0) AS D764,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 765), 0) AS D765,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 766), 0) AS D766,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 767), 0) AS D767,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 768), 0) AS D768,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 769), 0) AS D769,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 770), 0) AS D770,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 771), 0) AS D771,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 772), 0) AS D772,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 773), 0) AS D773,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 774), 0) AS D774,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 775), 0) AS D775,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 776), 0) AS D776,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 777), 0) AS D777,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 778), 0) AS D778,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 779), 0) AS D779,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 780), 0) AS D780,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 781), 0) AS D781,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 782), 0) AS D782,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 783), 0) AS D783,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 784), 0) AS D784,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 785), 0) AS D785,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 786), 0) AS D786,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 787), 0) AS D787,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 788), 0) AS D788,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 789), 0) AS D789,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 790), 0) AS D790,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 791), 0) AS D791,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 792), 0) AS D792,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 793), 0) AS D793,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 794), 0) AS D794,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 795), 0) AS D795,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 796), 0) AS D796,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 797), 0) AS D797,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 798), 0) AS D798,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 799), 0) AS D799,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 800), 0) AS D800,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 801), 0) AS D801,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 802), 0) AS D802,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 803), 0) AS D803,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 804), 0) AS D804,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 805), 0) AS D805,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 806), 0) AS D806,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 807), 0) AS D807,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 808), 0) AS D808,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 809), 0) AS D809,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 810), 0) AS D810,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 811), 0) AS D811,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 812), 0) AS D812,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 813), 0) AS D813,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 814), 0) AS D814,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 815), 0) AS D815,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 816), 0) AS D816,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 817), 0) AS D817,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 818), 0) AS D818,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 819), 0) AS D819,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 820), 0) AS D820,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 821), 0) AS D821,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 822), 0) AS D822,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 823), 0) AS D823,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 824), 0) AS D824,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 825), 0) AS D825,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 826), 0) AS D826,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 827), 0) AS D827,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 828), 0) AS D828,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 829), 0) AS D829,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 830), 0) AS D830,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 831), 0) AS D831,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 832), 0) AS D832,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 833), 0) AS D833,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 834), 0) AS D834,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 835), 0) AS D835,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 836), 0) AS D836,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 837), 0) AS D837,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 838), 0) AS D838,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 839), 0) AS D839,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 840), 0) AS D840,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 841), 0) AS D841,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 842), 0) AS D842,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 843), 0) AS D843,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 844), 0) AS D844,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 845), 0) AS D845,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 846), 0) AS D846,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 847), 0) AS D847,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 848), 0) AS D848,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 849), 0) AS D849,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 850), 0) AS D850,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 851), 0) AS D851,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 852), 0) AS D852,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 853), 0) AS D853,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 854), 0) AS D854,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 855), 0) AS D855,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 856), 0) AS D856,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 857), 0) AS D857,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 858), 0) AS D858,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 859), 0) AS D859,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 860), 0) AS D860,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 861), 0) AS D861,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 862), 0) AS D862,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 863), 0) AS D863,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 864), 0) AS D864,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 865), 0) AS D865,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 866), 0) AS D866,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 867), 0) AS D867,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 868), 0) AS D868,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 869), 0) AS D869,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 870), 0) AS D870,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 871), 0) AS D871,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 872), 0) AS D872,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 873), 0) AS D873,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 874), 0) AS D874,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 875), 0) AS D875,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 876), 0) AS D876,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 877), 0) AS D877,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 878), 0) AS D878,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 879), 0) AS D879,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 880), 0) AS D880,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 881), 0) AS D881,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 882), 0) AS D882,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 883), 0) AS D883,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 884), 0) AS D884,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 885), 0) AS D885,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 886), 0) AS D886,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 887), 0) AS D887,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 888), 0) AS D888,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 889), 0) AS D889,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 890), 0) AS D890,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 891), 0) AS D891,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 892), 0) AS D892,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 893), 0) AS D893,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 894), 0) AS D894,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 895), 0) AS D895,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 896), 0) AS D896,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 897), 0) AS D897,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 898), 0) AS D898,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 899), 0) AS D899,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 900), 0) AS D900,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 901), 0) AS D901,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 902), 0) AS D902,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 903), 0) AS D903,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 904), 0) AS D904,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 905), 0) AS D905,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 906), 0) AS D906,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 907), 0) AS D907,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 908), 0) AS D908,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 909), 0) AS D909,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 910), 0) AS D910,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 911), 0) AS D911,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 912), 0) AS D912,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 913), 0) AS D913,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 914), 0) AS D914,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 915), 0) AS D915,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 916), 0) AS D916,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 917), 0) AS D917,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 918), 0) AS D918,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 919), 0) AS D919,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 920), 0) AS D920,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 921), 0) AS D921,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 922), 0) AS D922,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 923), 0) AS D923,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 924), 0) AS D924,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 925), 0) AS D925,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 926), 0) AS D926,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 927), 0) AS D927,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 928), 0) AS D928,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 929), 0) AS D929,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 930), 0) AS D930,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 931), 0) AS D931,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 932), 0) AS D932,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 933), 0) AS D933,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 934), 0) AS D934,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 935), 0) AS D935,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 936), 0) AS D936,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 937), 0) AS D937,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 938), 0) AS D938,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 939), 0) AS D939,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 940), 0) AS D940,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 941), 0) AS D941,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 942), 0) AS D942,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 943), 0) AS D943,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 944), 0) AS D944,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 945), 0) AS D945,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 946), 0) AS D946,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 947), 0) AS D947,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 948), 0) AS D948,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 949), 0) AS D949,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 950), 0) AS D950,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 951), 0) AS D951,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 952), 0) AS D952,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 953), 0) AS D953,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 954), 0) AS D954,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 955), 0) AS D955,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 956), 0) AS D956,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 957), 0) AS D957,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 958), 0) AS D958,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 959), 0) AS D959,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 960), 0) AS D960,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 961), 0) AS D961,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 962), 0) AS D962,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 963), 0) AS D963,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 964), 0) AS D964,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 965), 0) AS D965,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 966), 0) AS D966,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 967), 0) AS D967,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 968), 0) AS D968,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 969), 0) AS D969,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 970), 0) AS D970,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 971), 0) AS D971,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 972), 0) AS D972,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 973), 0) AS D973,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 974), 0) AS D974,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 975), 0) AS D975,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 976), 0) AS D976,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 977), 0) AS D977,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 978), 0) AS D978,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 979), 0) AS D979,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 980), 0) AS D980,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 981), 0) AS D981,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 982), 0) AS D982,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 983), 0) AS D983,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 984), 0) AS D984,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 985), 0) AS D985,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 986), 0) AS D986,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 987), 0) AS D987,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 988), 0) AS D988,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 989), 0) AS D989,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 990), 0) AS D990,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 991), 0) AS D991,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 992), 0) AS D992,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 993), 0) AS D993,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 994), 0) AS D994,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 995), 0) AS D995,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 996), 0) AS D996,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 997), 0) AS D997,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 998), 0) AS D998,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 999), 0) AS D999,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 1000), 0) AS D1000,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 1001), 0) AS D1001,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 1002), 0) AS D1002,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 1003), 0) AS D1003,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 1004), 0) AS D1004,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 1005), 0) AS D1005,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 1006), 0) AS D1006,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 1007), 0) AS D1007,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 1008), 0) AS D1008,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 1009), 0) AS D1009,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 1010), 0) AS D1010,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 1011), 0) AS D1011,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 1012), 0) AS D1012,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 1013), 0) AS D1013,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 1014), 0) AS D1014,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 1015), 0) AS D1015,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 1016), 0) AS D1016,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 1017), 0) AS D1017,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 1018), 0) AS D1018,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 1019), 0) AS D1019,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 1020), 0) AS D1020,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 1021), 0) AS D1021,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 1022), 0) AS D1022,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 1023), 0) AS D1023,    
                   COALESCE((SELECT qty FROM die_deduct dd WHERE mm.rcno = dd.rcno AND mm.step = dd.step AND mm.wfid = dd.wfid AND dd.bin = 1024), 0) AS D1024  
              FROM mm),
     pb AS (SELECT 
                   CASE WHEN SUBSTR(vs.yield_bin,  1, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  1, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  1, 1)) - 48 END AS pb01,  
                   CASE WHEN SUBSTR(vs.yield_bin,  2, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  2, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  2, 1)) - 48 END AS pb02,  
                   CASE WHEN SUBSTR(vs.yield_bin,  3, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  3, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  3, 1)) - 48 END AS pb03,  
                   CASE WHEN SUBSTR(vs.yield_bin,  4, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  4, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  4, 1)) - 48 END AS pb04,  
                   CASE WHEN SUBSTR(vs.yield_bin,  5, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  5, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  5, 1)) - 48 END AS pb05,  
                   CASE WHEN SUBSTR(vs.yield_bin,  6, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  6, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  6, 1)) - 48 END AS pb06,  
                   CASE WHEN SUBSTR(vs.yield_bin,  7, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  7, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  7, 1)) - 48 END AS pb07,  
                   CASE WHEN SUBSTR(vs.yield_bin,  8, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  8, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  8, 1)) - 48 END AS pb08,  
                   CASE WHEN SUBSTR(vs.yield_bin,  9, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  9, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  9, 1)) - 48 END AS pb09,  
                   CASE WHEN SUBSTR(vs.yield_bin,  10, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  10, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  10, 1)) - 48 END AS pb10,  
                   CASE WHEN SUBSTR(vs.yield_bin,  11, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  11, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  11, 1)) - 48 END AS pb11,  
                   CASE WHEN SUBSTR(vs.yield_bin,  12, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  12, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  12, 1)) - 48 END AS pb12,  
                   CASE WHEN SUBSTR(vs.yield_bin,  13, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  13, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  13, 1)) - 48 END AS pb13,  
                   CASE WHEN SUBSTR(vs.yield_bin,  14, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  14, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  14, 1)) - 48 END AS pb14,  
                   CASE WHEN SUBSTR(vs.yield_bin,  15, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  15, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  15, 1)) - 48 END AS pb15,  
                   CASE WHEN SUBSTR(vs.yield_bin,  16, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  16, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  16, 1)) - 48 END AS pb16,  
                   CASE WHEN SUBSTR(vs.yield_bin,  17, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  17, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  17, 1)) - 48 END AS pb17,  
                   CASE WHEN SUBSTR(vs.yield_bin,  18, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  18, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  18, 1)) - 48 END AS pb18,  
                   CASE WHEN SUBSTR(vs.yield_bin,  19, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  19, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  19, 1)) - 48 END AS pb19,  
                   CASE WHEN SUBSTR(vs.yield_bin,  20, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  20, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  20, 1)) - 48 END AS pb20,  
                   CASE WHEN SUBSTR(vs.yield_bin,  21, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  21, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  21, 1)) - 48 END AS pb21,  
                   CASE WHEN SUBSTR(vs.yield_bin,  22, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  22, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  22, 1)) - 48 END AS pb22,  
                   CASE WHEN SUBSTR(vs.yield_bin,  23, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  23, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  23, 1)) - 48 END AS pb23,  
                   CASE WHEN SUBSTR(vs.yield_bin,  24, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  24, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  24, 1)) - 48 END AS pb24,  
                   CASE WHEN SUBSTR(vs.yield_bin,  25, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  25, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  25, 1)) - 48 END AS pb25,  
                   CASE WHEN SUBSTR(vs.yield_bin,  26, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  26, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  26, 1)) - 48 END AS pb26,  
                   CASE WHEN SUBSTR(vs.yield_bin,  27, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  27, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  27, 1)) - 48 END AS pb27,  
                   CASE WHEN SUBSTR(vs.yield_bin,  28, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  28, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  28, 1)) - 48 END AS pb28,  
                   CASE WHEN SUBSTR(vs.yield_bin,  29, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  29, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  29, 1)) - 48 END AS pb29,  
                   CASE WHEN SUBSTR(vs.yield_bin,  30, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  30, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  30, 1)) - 48 END AS pb30,  
                   CASE WHEN SUBSTR(vs.yield_bin,  31, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  31, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  31, 1)) - 48 END AS pb31,  
                   CASE WHEN SUBSTR(vs.yield_bin,  32, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  32, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  32, 1)) - 48 END AS pb32,  
                   CASE WHEN SUBSTR(vs.yield_bin,  33, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  33, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  33, 1)) - 48 END AS pb33,  
                   CASE WHEN SUBSTR(vs.yield_bin,  34, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  34, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  34, 1)) - 48 END AS pb34,  
                   CASE WHEN SUBSTR(vs.yield_bin,  35, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  35, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  35, 1)) - 48 END AS pb35,  
                   CASE WHEN SUBSTR(vs.yield_bin,  36, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  36, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  36, 1)) - 48 END AS pb36,  
                   CASE WHEN SUBSTR(vs.yield_bin,  37, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  37, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  37, 1)) - 48 END AS pb37,  
                   CASE WHEN SUBSTR(vs.yield_bin,  38, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  38, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  38, 1)) - 48 END AS pb38,  
                   CASE WHEN SUBSTR(vs.yield_bin,  39, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  39, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  39, 1)) - 48 END AS pb39,  
                   CASE WHEN SUBSTR(vs.yield_bin,  40, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  40, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  40, 1)) - 48 END AS pb40,  
                   CASE WHEN SUBSTR(vs.yield_bin,  41, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  41, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  41, 1)) - 48 END AS pb41,  
                   CASE WHEN SUBSTR(vs.yield_bin,  42, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  42, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  42, 1)) - 48 END AS pb42,  
                   CASE WHEN SUBSTR(vs.yield_bin,  43, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  43, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  43, 1)) - 48 END AS pb43,  
                   CASE WHEN SUBSTR(vs.yield_bin,  44, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  44, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  44, 1)) - 48 END AS pb44,  
                   CASE WHEN SUBSTR(vs.yield_bin,  45, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  45, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  45, 1)) - 48 END AS pb45,  
                   CASE WHEN SUBSTR(vs.yield_bin,  46, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  46, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  46, 1)) - 48 END AS pb46,  
                   CASE WHEN SUBSTR(vs.yield_bin,  47, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  47, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  47, 1)) - 48 END AS pb47,  
                   CASE WHEN SUBSTR(vs.yield_bin,  48, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  48, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  48, 1)) - 48 END AS pb48,  
                   CASE WHEN SUBSTR(vs.yield_bin,  49, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  49, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  49, 1)) - 48 END AS pb49,  
                   CASE WHEN SUBSTR(vs.yield_bin,  50, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  50, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  50, 1)) - 48 END AS pb50,  
                   CASE WHEN SUBSTR(vs.yield_bin,  51, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  51, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  51, 1)) - 48 END AS pb51,  
                   CASE WHEN SUBSTR(vs.yield_bin,  52, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  52, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  52, 1)) - 48 END AS pb52,  
                   CASE WHEN SUBSTR(vs.yield_bin,  53, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  53, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  53, 1)) - 48 END AS pb53,  
                   CASE WHEN SUBSTR(vs.yield_bin,  54, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  54, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  54, 1)) - 48 END AS pb54,  
                   CASE WHEN SUBSTR(vs.yield_bin,  55, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  55, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  55, 1)) - 48 END AS pb55,  
                   CASE WHEN SUBSTR(vs.yield_bin,  56, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  56, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  56, 1)) - 48 END AS pb56,  
                   CASE WHEN SUBSTR(vs.yield_bin,  57, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  57, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  57, 1)) - 48 END AS pb57,  
                   CASE WHEN SUBSTR(vs.yield_bin,  58, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  58, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  58, 1)) - 48 END AS pb58,  
                   CASE WHEN SUBSTR(vs.yield_bin,  59, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  59, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  59, 1)) - 48 END AS pb59,  
                   CASE WHEN SUBSTR(vs.yield_bin,  60, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  60, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  60, 1)) - 48 END AS pb60,  
                   CASE WHEN SUBSTR(vs.yield_bin,  61, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  61, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  61, 1)) - 48 END AS pb61,  
                   CASE WHEN SUBSTR(vs.yield_bin,  62, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  62, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  62, 1)) - 48 END AS pb62,  
                   CASE WHEN SUBSTR(vs.yield_bin,  63, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  63, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  63, 1)) - 48 END AS pb63,  
                   CASE WHEN SUBSTR(vs.yield_bin,  64, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  64, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  64, 1)) - 48 END AS pb64,  
                   CASE WHEN SUBSTR(vs.yield_bin,  65, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  65, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  65, 1)) - 48 END AS pb65,  
                   CASE WHEN SUBSTR(vs.yield_bin,  66, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  66, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  66, 1)) - 48 END AS pb66,  
                   CASE WHEN SUBSTR(vs.yield_bin,  67, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  67, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  67, 1)) - 48 END AS pb67,  
                   CASE WHEN SUBSTR(vs.yield_bin,  68, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  68, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  68, 1)) - 48 END AS pb68,  
                   CASE WHEN SUBSTR(vs.yield_bin,  69, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  69, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  69, 1)) - 48 END AS pb69,  
                   CASE WHEN SUBSTR(vs.yield_bin,  70, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  70, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  70, 1)) - 48 END AS pb70,  
                   CASE WHEN SUBSTR(vs.yield_bin,  71, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  71, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  71, 1)) - 48 END AS pb71,  
                   CASE WHEN SUBSTR(vs.yield_bin,  72, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  72, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  72, 1)) - 48 END AS pb72,  
                   CASE WHEN SUBSTR(vs.yield_bin,  73, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  73, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  73, 1)) - 48 END AS pb73,  
                   CASE WHEN SUBSTR(vs.yield_bin,  74, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  74, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  74, 1)) - 48 END AS pb74,  
                   CASE WHEN SUBSTR(vs.yield_bin,  75, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  75, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  75, 1)) - 48 END AS pb75,  
                   CASE WHEN SUBSTR(vs.yield_bin,  76, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  76, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  76, 1)) - 48 END AS pb76,  
                   CASE WHEN SUBSTR(vs.yield_bin,  77, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  77, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  77, 1)) - 48 END AS pb77,  
                   CASE WHEN SUBSTR(vs.yield_bin,  78, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  78, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  78, 1)) - 48 END AS pb78,  
                   CASE WHEN SUBSTR(vs.yield_bin,  79, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  79, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  79, 1)) - 48 END AS pb79,  
                   CASE WHEN SUBSTR(vs.yield_bin,  80, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  80, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  80, 1)) - 48 END AS pb80,  
                   CASE WHEN SUBSTR(vs.yield_bin,  81, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  81, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  81, 1)) - 48 END AS pb81,  
                   CASE WHEN SUBSTR(vs.yield_bin,  82, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  82, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  82, 1)) - 48 END AS pb82,  
                   CASE WHEN SUBSTR(vs.yield_bin,  83, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  83, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  83, 1)) - 48 END AS pb83,  
                   CASE WHEN SUBSTR(vs.yield_bin,  84, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  84, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  84, 1)) - 48 END AS pb84,  
                   CASE WHEN SUBSTR(vs.yield_bin,  85, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  85, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  85, 1)) - 48 END AS pb85,  
                   CASE WHEN SUBSTR(vs.yield_bin,  86, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  86, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  86, 1)) - 48 END AS pb86,  
                   CASE WHEN SUBSTR(vs.yield_bin,  87, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  87, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  87, 1)) - 48 END AS pb87,  
                   CASE WHEN SUBSTR(vs.yield_bin,  88, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  88, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  88, 1)) - 48 END AS pb88,  
                   CASE WHEN SUBSTR(vs.yield_bin,  89, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  89, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  89, 1)) - 48 END AS pb89,  
                   CASE WHEN SUBSTR(vs.yield_bin,  90, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  90, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  90, 1)) - 48 END AS pb90,  
                   CASE WHEN SUBSTR(vs.yield_bin,  91, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  91, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  91, 1)) - 48 END AS pb91,  
                   CASE WHEN SUBSTR(vs.yield_bin,  92, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  92, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  92, 1)) - 48 END AS pb92,  
                   CASE WHEN SUBSTR(vs.yield_bin,  93, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  93, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  93, 1)) - 48 END AS pb93,  
                   CASE WHEN SUBSTR(vs.yield_bin,  94, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  94, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  94, 1)) - 48 END AS pb94,  
                   CASE WHEN SUBSTR(vs.yield_bin,  95, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  95, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  95, 1)) - 48 END AS pb95,  
                   CASE WHEN SUBSTR(vs.yield_bin,  96, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  96, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  96, 1)) - 48 END AS pb96,  
                   CASE WHEN SUBSTR(vs.yield_bin,  97, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  97, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  97, 1)) - 48 END AS pb97,  
                   CASE WHEN SUBSTR(vs.yield_bin,  98, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  98, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  98, 1)) - 48 END AS pb98,  
                   CASE WHEN SUBSTR(vs.yield_bin,  99, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  99, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  99, 1)) - 48 END AS pb99,  
                   CASE WHEN SUBSTR(vs.yield_bin,  100, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  100, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  100, 1)) - 48 END AS pb100,  
                   CASE WHEN SUBSTR(vs.yield_bin,  101, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  101, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  101, 1)) - 48 END AS pb101,  
                   CASE WHEN SUBSTR(vs.yield_bin,  102, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  102, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  102, 1)) - 48 END AS pb102,  
                   CASE WHEN SUBSTR(vs.yield_bin,  103, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  103, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  103, 1)) - 48 END AS pb103,  
                   CASE WHEN SUBSTR(vs.yield_bin,  104, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  104, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  104, 1)) - 48 END AS pb104,  
                   CASE WHEN SUBSTR(vs.yield_bin,  105, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  105, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  105, 1)) - 48 END AS pb105,  
                   CASE WHEN SUBSTR(vs.yield_bin,  106, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  106, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  106, 1)) - 48 END AS pb106,  
                   CASE WHEN SUBSTR(vs.yield_bin,  107, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  107, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  107, 1)) - 48 END AS pb107,  
                   CASE WHEN SUBSTR(vs.yield_bin,  108, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  108, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  108, 1)) - 48 END AS pb108,  
                   CASE WHEN SUBSTR(vs.yield_bin,  109, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  109, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  109, 1)) - 48 END AS pb109,  
                   CASE WHEN SUBSTR(vs.yield_bin,  110, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  110, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  110, 1)) - 48 END AS pb110,  
                   CASE WHEN SUBSTR(vs.yield_bin,  111, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  111, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  111, 1)) - 48 END AS pb111,  
                   CASE WHEN SUBSTR(vs.yield_bin,  112, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  112, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  112, 1)) - 48 END AS pb112,  
                   CASE WHEN SUBSTR(vs.yield_bin,  113, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  113, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  113, 1)) - 48 END AS pb113,  
                   CASE WHEN SUBSTR(vs.yield_bin,  114, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  114, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  114, 1)) - 48 END AS pb114,  
                   CASE WHEN SUBSTR(vs.yield_bin,  115, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  115, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  115, 1)) - 48 END AS pb115,  
                   CASE WHEN SUBSTR(vs.yield_bin,  116, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  116, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  116, 1)) - 48 END AS pb116,  
                   CASE WHEN SUBSTR(vs.yield_bin,  117, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  117, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  117, 1)) - 48 END AS pb117,  
                   CASE WHEN SUBSTR(vs.yield_bin,  118, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  118, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  118, 1)) - 48 END AS pb118,  
                   CASE WHEN SUBSTR(vs.yield_bin,  119, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  119, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  119, 1)) - 48 END AS pb119,  
                   CASE WHEN SUBSTR(vs.yield_bin,  120, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  120, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  120, 1)) - 48 END AS pb120,  
                   CASE WHEN SUBSTR(vs.yield_bin,  121, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  121, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  121, 1)) - 48 END AS pb121,  
                   CASE WHEN SUBSTR(vs.yield_bin,  122, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  122, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  122, 1)) - 48 END AS pb122,  
                   CASE WHEN SUBSTR(vs.yield_bin,  123, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  123, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  123, 1)) - 48 END AS pb123,  
                   CASE WHEN SUBSTR(vs.yield_bin,  124, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  124, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  124, 1)) - 48 END AS pb124,  
                   CASE WHEN SUBSTR(vs.yield_bin,  125, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  125, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  125, 1)) - 48 END AS pb125,  
                   CASE WHEN SUBSTR(vs.yield_bin,  126, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  126, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  126, 1)) - 48 END AS pb126,  
                   CASE WHEN SUBSTR(vs.yield_bin,  127, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  127, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  127, 1)) - 48 END AS pb127,  
                   CASE WHEN SUBSTR(vs.yield_bin,  128, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  128, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  128, 1)) - 48 END AS pb128,  
                   CASE WHEN SUBSTR(vs.yield_bin,  129, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  129, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  129, 1)) - 48 END AS pb129,  
                   CASE WHEN SUBSTR(vs.yield_bin,  130, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  130, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  130, 1)) - 48 END AS pb130,  
                   CASE WHEN SUBSTR(vs.yield_bin,  131, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  131, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  131, 1)) - 48 END AS pb131,  
                   CASE WHEN SUBSTR(vs.yield_bin,  132, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  132, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  132, 1)) - 48 END AS pb132,  
                   CASE WHEN SUBSTR(vs.yield_bin,  133, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  133, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  133, 1)) - 48 END AS pb133,  
                   CASE WHEN SUBSTR(vs.yield_bin,  134, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  134, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  134, 1)) - 48 END AS pb134,  
                   CASE WHEN SUBSTR(vs.yield_bin,  135, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  135, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  135, 1)) - 48 END AS pb135,  
                   CASE WHEN SUBSTR(vs.yield_bin,  136, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  136, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  136, 1)) - 48 END AS pb136,  
                   CASE WHEN SUBSTR(vs.yield_bin,  137, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  137, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  137, 1)) - 48 END AS pb137,  
                   CASE WHEN SUBSTR(vs.yield_bin,  138, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  138, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  138, 1)) - 48 END AS pb138,  
                   CASE WHEN SUBSTR(vs.yield_bin,  139, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  139, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  139, 1)) - 48 END AS pb139,  
                   CASE WHEN SUBSTR(vs.yield_bin,  140, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  140, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  140, 1)) - 48 END AS pb140,  
                   CASE WHEN SUBSTR(vs.yield_bin,  141, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  141, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  141, 1)) - 48 END AS pb141,  
                   CASE WHEN SUBSTR(vs.yield_bin,  142, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  142, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  142, 1)) - 48 END AS pb142,  
                   CASE WHEN SUBSTR(vs.yield_bin,  143, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  143, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  143, 1)) - 48 END AS pb143,  
                   CASE WHEN SUBSTR(vs.yield_bin,  144, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  144, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  144, 1)) - 48 END AS pb144,  
                   CASE WHEN SUBSTR(vs.yield_bin,  145, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  145, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  145, 1)) - 48 END AS pb145,  
                   CASE WHEN SUBSTR(vs.yield_bin,  146, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  146, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  146, 1)) - 48 END AS pb146,  
                   CASE WHEN SUBSTR(vs.yield_bin,  147, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  147, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  147, 1)) - 48 END AS pb147,  
                   CASE WHEN SUBSTR(vs.yield_bin,  148, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  148, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  148, 1)) - 48 END AS pb148,  
                   CASE WHEN SUBSTR(vs.yield_bin,  149, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  149, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  149, 1)) - 48 END AS pb149,  
                   CASE WHEN SUBSTR(vs.yield_bin,  150, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  150, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  150, 1)) - 48 END AS pb150,  
                   CASE WHEN SUBSTR(vs.yield_bin,  151, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  151, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  151, 1)) - 48 END AS pb151,  
                   CASE WHEN SUBSTR(vs.yield_bin,  152, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  152, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  152, 1)) - 48 END AS pb152,  
                   CASE WHEN SUBSTR(vs.yield_bin,  153, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  153, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  153, 1)) - 48 END AS pb153,  
                   CASE WHEN SUBSTR(vs.yield_bin,  154, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  154, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  154, 1)) - 48 END AS pb154,  
                   CASE WHEN SUBSTR(vs.yield_bin,  155, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  155, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  155, 1)) - 48 END AS pb155,  
                   CASE WHEN SUBSTR(vs.yield_bin,  156, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  156, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  156, 1)) - 48 END AS pb156,  
                   CASE WHEN SUBSTR(vs.yield_bin,  157, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  157, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  157, 1)) - 48 END AS pb157,  
                   CASE WHEN SUBSTR(vs.yield_bin,  158, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  158, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  158, 1)) - 48 END AS pb158,  
                   CASE WHEN SUBSTR(vs.yield_bin,  159, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  159, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  159, 1)) - 48 END AS pb159,  
                   CASE WHEN SUBSTR(vs.yield_bin,  160, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  160, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  160, 1)) - 48 END AS pb160,  
                   CASE WHEN SUBSTR(vs.yield_bin,  161, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  161, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  161, 1)) - 48 END AS pb161,  
                   CASE WHEN SUBSTR(vs.yield_bin,  162, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  162, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  162, 1)) - 48 END AS pb162,  
                   CASE WHEN SUBSTR(vs.yield_bin,  163, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  163, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  163, 1)) - 48 END AS pb163,  
                   CASE WHEN SUBSTR(vs.yield_bin,  164, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  164, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  164, 1)) - 48 END AS pb164,  
                   CASE WHEN SUBSTR(vs.yield_bin,  165, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  165, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  165, 1)) - 48 END AS pb165,  
                   CASE WHEN SUBSTR(vs.yield_bin,  166, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  166, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  166, 1)) - 48 END AS pb166,  
                   CASE WHEN SUBSTR(vs.yield_bin,  167, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  167, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  167, 1)) - 48 END AS pb167,  
                   CASE WHEN SUBSTR(vs.yield_bin,  168, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  168, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  168, 1)) - 48 END AS pb168,  
                   CASE WHEN SUBSTR(vs.yield_bin,  169, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  169, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  169, 1)) - 48 END AS pb169,  
                   CASE WHEN SUBSTR(vs.yield_bin,  170, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  170, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  170, 1)) - 48 END AS pb170,  
                   CASE WHEN SUBSTR(vs.yield_bin,  171, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  171, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  171, 1)) - 48 END AS pb171,  
                   CASE WHEN SUBSTR(vs.yield_bin,  172, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  172, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  172, 1)) - 48 END AS pb172,  
                   CASE WHEN SUBSTR(vs.yield_bin,  173, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  173, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  173, 1)) - 48 END AS pb173,  
                   CASE WHEN SUBSTR(vs.yield_bin,  174, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  174, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  174, 1)) - 48 END AS pb174,  
                   CASE WHEN SUBSTR(vs.yield_bin,  175, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  175, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  175, 1)) - 48 END AS pb175,  
                   CASE WHEN SUBSTR(vs.yield_bin,  176, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  176, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  176, 1)) - 48 END AS pb176,  
                   CASE WHEN SUBSTR(vs.yield_bin,  177, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  177, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  177, 1)) - 48 END AS pb177,  
                   CASE WHEN SUBSTR(vs.yield_bin,  178, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  178, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  178, 1)) - 48 END AS pb178,  
                   CASE WHEN SUBSTR(vs.yield_bin,  179, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  179, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  179, 1)) - 48 END AS pb179,  
                   CASE WHEN SUBSTR(vs.yield_bin,  180, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  180, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  180, 1)) - 48 END AS pb180,  
                   CASE WHEN SUBSTR(vs.yield_bin,  181, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  181, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  181, 1)) - 48 END AS pb181,  
                   CASE WHEN SUBSTR(vs.yield_bin,  182, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  182, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  182, 1)) - 48 END AS pb182,  
                   CASE WHEN SUBSTR(vs.yield_bin,  183, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  183, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  183, 1)) - 48 END AS pb183,  
                   CASE WHEN SUBSTR(vs.yield_bin,  184, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  184, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  184, 1)) - 48 END AS pb184,  
                   CASE WHEN SUBSTR(vs.yield_bin,  185, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  185, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  185, 1)) - 48 END AS pb185,  
                   CASE WHEN SUBSTR(vs.yield_bin,  186, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  186, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  186, 1)) - 48 END AS pb186,  
                   CASE WHEN SUBSTR(vs.yield_bin,  187, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  187, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  187, 1)) - 48 END AS pb187,  
                   CASE WHEN SUBSTR(vs.yield_bin,  188, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  188, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  188, 1)) - 48 END AS pb188,  
                   CASE WHEN SUBSTR(vs.yield_bin,  189, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  189, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  189, 1)) - 48 END AS pb189,  
                   CASE WHEN SUBSTR(vs.yield_bin,  190, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  190, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  190, 1)) - 48 END AS pb190,  
                   CASE WHEN SUBSTR(vs.yield_bin,  191, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  191, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  191, 1)) - 48 END AS pb191,  
                   CASE WHEN SUBSTR(vs.yield_bin,  192, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  192, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  192, 1)) - 48 END AS pb192,  
                   CASE WHEN SUBSTR(vs.yield_bin,  193, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  193, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  193, 1)) - 48 END AS pb193,  
                   CASE WHEN SUBSTR(vs.yield_bin,  194, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  194, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  194, 1)) - 48 END AS pb194,  
                   CASE WHEN SUBSTR(vs.yield_bin,  195, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  195, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  195, 1)) - 48 END AS pb195,  
                   CASE WHEN SUBSTR(vs.yield_bin,  196, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  196, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  196, 1)) - 48 END AS pb196,  
                   CASE WHEN SUBSTR(vs.yield_bin,  197, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  197, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  197, 1)) - 48 END AS pb197,  
                   CASE WHEN SUBSTR(vs.yield_bin,  198, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  198, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  198, 1)) - 48 END AS pb198,  
                   CASE WHEN SUBSTR(vs.yield_bin,  199, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  199, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  199, 1)) - 48 END AS pb199,  
                   CASE WHEN SUBSTR(vs.yield_bin,  200, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  200, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  200, 1)) - 48 END AS pb200,  
                   CASE WHEN SUBSTR(vs.yield_bin,  201, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  201, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  201, 1)) - 48 END AS pb201,  
                   CASE WHEN SUBSTR(vs.yield_bin,  202, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  202, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  202, 1)) - 48 END AS pb202,  
                   CASE WHEN SUBSTR(vs.yield_bin,  203, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  203, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  203, 1)) - 48 END AS pb203,  
                   CASE WHEN SUBSTR(vs.yield_bin,  204, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  204, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  204, 1)) - 48 END AS pb204,  
                   CASE WHEN SUBSTR(vs.yield_bin,  205, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  205, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  205, 1)) - 48 END AS pb205,  
                   CASE WHEN SUBSTR(vs.yield_bin,  206, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  206, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  206, 1)) - 48 END AS pb206,  
                   CASE WHEN SUBSTR(vs.yield_bin,  207, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  207, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  207, 1)) - 48 END AS pb207,  
                   CASE WHEN SUBSTR(vs.yield_bin,  208, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  208, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  208, 1)) - 48 END AS pb208,  
                   CASE WHEN SUBSTR(vs.yield_bin,  209, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  209, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  209, 1)) - 48 END AS pb209,  
                   CASE WHEN SUBSTR(vs.yield_bin,  210, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  210, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  210, 1)) - 48 END AS pb210,  
                   CASE WHEN SUBSTR(vs.yield_bin,  211, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  211, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  211, 1)) - 48 END AS pb211,  
                   CASE WHEN SUBSTR(vs.yield_bin,  212, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  212, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  212, 1)) - 48 END AS pb212,  
                   CASE WHEN SUBSTR(vs.yield_bin,  213, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  213, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  213, 1)) - 48 END AS pb213,  
                   CASE WHEN SUBSTR(vs.yield_bin,  214, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  214, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  214, 1)) - 48 END AS pb214,  
                   CASE WHEN SUBSTR(vs.yield_bin,  215, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  215, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  215, 1)) - 48 END AS pb215,  
                   CASE WHEN SUBSTR(vs.yield_bin,  216, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  216, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  216, 1)) - 48 END AS pb216,  
                   CASE WHEN SUBSTR(vs.yield_bin,  217, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  217, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  217, 1)) - 48 END AS pb217,  
                   CASE WHEN SUBSTR(vs.yield_bin,  218, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  218, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  218, 1)) - 48 END AS pb218,  
                   CASE WHEN SUBSTR(vs.yield_bin,  219, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  219, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  219, 1)) - 48 END AS pb219,  
                   CASE WHEN SUBSTR(vs.yield_bin,  220, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  220, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  220, 1)) - 48 END AS pb220,  
                   CASE WHEN SUBSTR(vs.yield_bin,  221, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  221, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  221, 1)) - 48 END AS pb221,  
                   CASE WHEN SUBSTR(vs.yield_bin,  222, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  222, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  222, 1)) - 48 END AS pb222,  
                   CASE WHEN SUBSTR(vs.yield_bin,  223, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  223, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  223, 1)) - 48 END AS pb223,  
                   CASE WHEN SUBSTR(vs.yield_bin,  224, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  224, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  224, 1)) - 48 END AS pb224,  
                   CASE WHEN SUBSTR(vs.yield_bin,  225, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  225, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  225, 1)) - 48 END AS pb225,  
                   CASE WHEN SUBSTR(vs.yield_bin,  226, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  226, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  226, 1)) - 48 END AS pb226,  
                   CASE WHEN SUBSTR(vs.yield_bin,  227, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  227, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  227, 1)) - 48 END AS pb227,  
                   CASE WHEN SUBSTR(vs.yield_bin,  228, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  228, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  228, 1)) - 48 END AS pb228,  
                   CASE WHEN SUBSTR(vs.yield_bin,  229, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  229, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  229, 1)) - 48 END AS pb229,  
                   CASE WHEN SUBSTR(vs.yield_bin,  230, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  230, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  230, 1)) - 48 END AS pb230,  
                   CASE WHEN SUBSTR(vs.yield_bin,  231, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  231, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  231, 1)) - 48 END AS pb231,  
                   CASE WHEN SUBSTR(vs.yield_bin,  232, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  232, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  232, 1)) - 48 END AS pb232,  
                   CASE WHEN SUBSTR(vs.yield_bin,  233, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  233, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  233, 1)) - 48 END AS pb233,  
                   CASE WHEN SUBSTR(vs.yield_bin,  234, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  234, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  234, 1)) - 48 END AS pb234,  
                   CASE WHEN SUBSTR(vs.yield_bin,  235, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  235, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  235, 1)) - 48 END AS pb235,  
                   CASE WHEN SUBSTR(vs.yield_bin,  236, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  236, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  236, 1)) - 48 END AS pb236,  
                   CASE WHEN SUBSTR(vs.yield_bin,  237, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  237, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  237, 1)) - 48 END AS pb237,  
                   CASE WHEN SUBSTR(vs.yield_bin,  238, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  238, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  238, 1)) - 48 END AS pb238,  
                   CASE WHEN SUBSTR(vs.yield_bin,  239, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  239, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  239, 1)) - 48 END AS pb239,  
                   CASE WHEN SUBSTR(vs.yield_bin,  240, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  240, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  240, 1)) - 48 END AS pb240,  
                   CASE WHEN SUBSTR(vs.yield_bin,  241, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  241, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  241, 1)) - 48 END AS pb241,  
                   CASE WHEN SUBSTR(vs.yield_bin,  242, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  242, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  242, 1)) - 48 END AS pb242,  
                   CASE WHEN SUBSTR(vs.yield_bin,  243, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  243, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  243, 1)) - 48 END AS pb243,  
                   CASE WHEN SUBSTR(vs.yield_bin,  244, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  244, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  244, 1)) - 48 END AS pb244,  
                   CASE WHEN SUBSTR(vs.yield_bin,  245, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  245, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  245, 1)) - 48 END AS pb245,  
                   CASE WHEN SUBSTR(vs.yield_bin,  246, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  246, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  246, 1)) - 48 END AS pb246,  
                   CASE WHEN SUBSTR(vs.yield_bin,  247, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  247, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  247, 1)) - 48 END AS pb247,  
                   CASE WHEN SUBSTR(vs.yield_bin,  248, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  248, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  248, 1)) - 48 END AS pb248,  
                   CASE WHEN SUBSTR(vs.yield_bin,  249, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  249, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  249, 1)) - 48 END AS pb249,  
                   CASE WHEN SUBSTR(vs.yield_bin,  250, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  250, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  250, 1)) - 48 END AS pb250,  
                   CASE WHEN SUBSTR(vs.yield_bin,  251, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  251, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  251, 1)) - 48 END AS pb251,  
                   CASE WHEN SUBSTR(vs.yield_bin,  252, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  252, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  252, 1)) - 48 END AS pb252,  
                   CASE WHEN SUBSTR(vs.yield_bin,  253, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  253, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  253, 1)) - 48 END AS pb253,  
                   CASE WHEN SUBSTR(vs.yield_bin,  254, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  254, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  254, 1)) - 48 END AS pb254,  
                   CASE WHEN SUBSTR(vs.yield_bin,  255, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  255, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  255, 1)) - 48 END AS pb255,  
                   CASE WHEN SUBSTR(vs.yield_bin,  256, 1) > '9' THEN ASCII(SUBSTR(vs.yield_bin,  256, 1)) - 55 ELSE ASCII(SUBSTR(vs.yield_bin,  256, 1)) - 48 END AS pb256,  
               ARRAY_TO_STRING(ARRAY((SELECT CAST(mapping_value AS INTEGER) + ((generate_series - 1) * 4) 
                  FROM list_value_detail, 
                       GENERATE_SERIES(1, LENGTH(vs.yield_bin)) 
                 WHERE type_id LIKE 'HEX_BIT_%'  
                   AND code_id = SUBSTR(vs.yield_bin, generate_series, 1) 
                 ORDER BY 1)), ',') AS pass_bin_set 
          FROM ( select yield_bin, yield_step from view_sinfo 
                    where seqno=Cast('99847220' as Int) 
                ) vs ), 
     yld_testdie as (SELECT SUM(
           CASE WHEN MOD(pb.pb01, 16)> 7 THEN COALESCE(mm.b01,  0) ELSE 0 END + CASE WHEN MOD(pb.pb01, 8) > 3 THEN COALESCE(mm.b02,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb01, 4) > 1 THEN COALESCE(mm.b03,  0) ELSE 0 END + CASE WHEN MOD(pb.pb01, 2) = 1 THEN COALESCE(mm.b04,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb02, 16)> 7 THEN COALESCE(mm.b05,  0) ELSE 0 END + CASE WHEN MOD(pb.pb02, 8) > 3 THEN COALESCE(mm.b06,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb02, 4) > 1 THEN COALESCE(mm.b07,  0) ELSE 0 END + CASE WHEN MOD(pb.pb02, 2) = 1 THEN COALESCE(mm.b08,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb03, 16)> 7 THEN COALESCE(mm.b09,  0) ELSE 0 END + CASE WHEN MOD(pb.pb03, 8) > 3 THEN COALESCE(mm.b10,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb03, 4) > 1 THEN COALESCE(mm.b11,  0) ELSE 0 END + CASE WHEN MOD(pb.pb03, 2) = 1 THEN COALESCE(mm.b12,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb04, 16)> 7 THEN COALESCE(mm.b13,  0) ELSE 0 END + CASE WHEN MOD(pb.pb04, 8) > 3 THEN COALESCE(mm.b14,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb04, 4) > 1 THEN COALESCE(mm.b15,  0) ELSE 0 END + CASE WHEN MOD(pb.pb04, 2) = 1 THEN COALESCE(mm.b16,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb05, 16)> 7 THEN COALESCE(mm.b17,  0) ELSE 0 END + CASE WHEN MOD(pb.pb05, 8) > 3 THEN COALESCE(mm.b18,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb05, 4) > 1 THEN COALESCE(mm.b19,  0) ELSE 0 END + CASE WHEN MOD(pb.pb05, 2) = 1 THEN COALESCE(mm.b20,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb06, 16)> 7 THEN COALESCE(mm.b21,  0) ELSE 0 END + CASE WHEN MOD(pb.pb06, 8) > 3 THEN COALESCE(mm.b22,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb06, 4) > 1 THEN COALESCE(mm.b23,  0) ELSE 0 END + CASE WHEN MOD(pb.pb06, 2) = 1 THEN COALESCE(mm.b24,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb07, 16)> 7 THEN COALESCE(mm.b25,  0) ELSE 0 END + CASE WHEN MOD(pb.pb07, 8) > 3 THEN COALESCE(mm.b26,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb07, 4) > 1 THEN COALESCE(mm.b27,  0) ELSE 0 END + CASE WHEN MOD(pb.pb07, 2) = 1 THEN COALESCE(mm.b28,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb08, 16)> 7 THEN COALESCE(mm.b29,  0) ELSE 0 END + CASE WHEN MOD(pb.pb08, 8) > 3 THEN COALESCE(mm.b30,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb08, 4) > 1 THEN COALESCE(mm.b31,  0) ELSE 0 END + CASE WHEN MOD(pb.pb08, 2) = 1 THEN COALESCE(mm.b32,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb09, 16)> 7 THEN COALESCE(mm.b33,  0) ELSE 0 END + CASE WHEN MOD(pb.pb09, 8) > 3 THEN COALESCE(mm.b34,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb09, 4) > 1 THEN COALESCE(mm.b35,  0) ELSE 0 END + CASE WHEN MOD(pb.pb09, 2) = 1 THEN COALESCE(mm.b36,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb10, 16)> 7 THEN COALESCE(mm.b37,  0) ELSE 0 END + CASE WHEN MOD(pb.pb10, 8) > 3 THEN COALESCE(mm.b38,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb10, 4) > 1 THEN COALESCE(mm.b39,  0) ELSE 0 END + CASE WHEN MOD(pb.pb10, 2) = 1 THEN COALESCE(mm.b40,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb11, 16)> 7 THEN COALESCE(mm.b41,  0) ELSE 0 END + CASE WHEN MOD(pb.pb11, 8) > 3 THEN COALESCE(mm.b42,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb11, 4) > 1 THEN COALESCE(mm.b43,  0) ELSE 0 END + CASE WHEN MOD(pb.pb11, 2) = 1 THEN COALESCE(mm.b44,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb12, 16)> 7 THEN COALESCE(mm.b45,  0) ELSE 0 END + CASE WHEN MOD(pb.pb12, 8) > 3 THEN COALESCE(mm.b46,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb12, 4) > 1 THEN COALESCE(mm.b47,  0) ELSE 0 END + CASE WHEN MOD(pb.pb12, 2) = 1 THEN COALESCE(mm.b48,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb13, 16)> 7 THEN COALESCE(mm.b49,  0) ELSE 0 END + CASE WHEN MOD(pb.pb13, 8) > 3 THEN COALESCE(mm.b50,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb13, 4) > 1 THEN COALESCE(mm.b51,  0) ELSE 0 END + CASE WHEN MOD(pb.pb13, 2) = 1 THEN COALESCE(mm.b52,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb14, 16)> 7 THEN COALESCE(mm.b53,  0) ELSE 0 END + CASE WHEN MOD(pb.pb14, 8) > 3 THEN COALESCE(mm.b54,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb14, 4) > 1 THEN COALESCE(mm.b55,  0) ELSE 0 END + CASE WHEN MOD(pb.pb14, 2) = 1 THEN COALESCE(mm.b56,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb15, 16)> 7 THEN COALESCE(mm.b57,  0) ELSE 0 END + CASE WHEN MOD(pb.pb15, 8) > 3 THEN COALESCE(mm.b58,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb15, 4) > 1 THEN COALESCE(mm.b59,  0) ELSE 0 END + CASE WHEN MOD(pb.pb15, 2) = 1 THEN COALESCE(mm.b60,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb16, 16)> 7 THEN COALESCE(mm.b61,  0) ELSE 0 END + CASE WHEN MOD(pb.pb16, 8) > 3 THEN COALESCE(mm.b62,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb16, 4) > 1 THEN COALESCE(mm.b63,  0) ELSE 0 END + CASE WHEN MOD(pb.pb16, 2) = 1 THEN COALESCE(mm.b64,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb17, 16)> 7 THEN COALESCE(mm.b65,  0) ELSE 0 END + CASE WHEN MOD(pb.pb17, 8) > 3 THEN COALESCE(mm.b66,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb17, 4) > 1 THEN COALESCE(mm.b67,  0) ELSE 0 END + CASE WHEN MOD(pb.pb17, 2) = 1 THEN COALESCE(mm.b68,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb18, 16)> 7 THEN COALESCE(mm.b69,  0) ELSE 0 END + CASE WHEN MOD(pb.pb18, 8) > 3 THEN COALESCE(mm.b70,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb18, 4) > 1 THEN COALESCE(mm.b71,  0) ELSE 0 END + CASE WHEN MOD(pb.pb18, 2) = 1 THEN COALESCE(mm.b72,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb19, 16)> 7 THEN COALESCE(mm.b73,  0) ELSE 0 END + CASE WHEN MOD(pb.pb19, 8) > 3 THEN COALESCE(mm.b74,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb19, 4) > 1 THEN COALESCE(mm.b75,  0) ELSE 0 END + CASE WHEN MOD(pb.pb19, 2) = 1 THEN COALESCE(mm.b76,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb20, 16)> 7 THEN COALESCE(mm.b77,  0) ELSE 0 END + CASE WHEN MOD(pb.pb20, 8) > 3 THEN COALESCE(mm.b78,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb20, 4) > 1 THEN COALESCE(mm.b79,  0) ELSE 0 END + CASE WHEN MOD(pb.pb20, 2) = 1 THEN COALESCE(mm.b80,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb21, 16)> 7 THEN COALESCE(mm.b81,  0) ELSE 0 END + CASE WHEN MOD(pb.pb21, 8) > 3 THEN COALESCE(mm.b82,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb21, 4) > 1 THEN COALESCE(mm.b83,  0) ELSE 0 END + CASE WHEN MOD(pb.pb21, 2) = 1 THEN COALESCE(mm.b84,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb22, 16)> 7 THEN COALESCE(mm.b85,  0) ELSE 0 END + CASE WHEN MOD(pb.pb22, 8) > 3 THEN COALESCE(mm.b86,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb22, 4) > 1 THEN COALESCE(mm.b87,  0) ELSE 0 END + CASE WHEN MOD(pb.pb22, 2) = 1 THEN COALESCE(mm.b88,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb23, 16)> 7 THEN COALESCE(mm.b89,  0) ELSE 0 END + CASE WHEN MOD(pb.pb23, 8) > 3 THEN COALESCE(mm.b90,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb23, 4) > 1 THEN COALESCE(mm.b91,  0) ELSE 0 END + CASE WHEN MOD(pb.pb23, 2) = 1 THEN COALESCE(mm.b92,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb24, 16)> 7 THEN COALESCE(mm.b93,  0) ELSE 0 END + CASE WHEN MOD(pb.pb24, 8) > 3 THEN COALESCE(mm.b94,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb24, 4) > 1 THEN COALESCE(mm.b95,  0) ELSE 0 END + CASE WHEN MOD(pb.pb24, 2) = 1 THEN COALESCE(mm.b96,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb25, 16)> 7 THEN COALESCE(mm.b97,  0) ELSE 0 END + CASE WHEN MOD(pb.pb25, 8) > 3 THEN COALESCE(mm.b98,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb25, 4) > 1 THEN COALESCE(mm.b99,  0) ELSE 0 END + CASE WHEN MOD(pb.pb25, 2) = 1 THEN COALESCE(mm.b100,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb26, 16)> 7 THEN COALESCE(mm.b101,  0) ELSE 0 END + CASE WHEN MOD(pb.pb26, 8) > 3 THEN COALESCE(mm.b102,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb26, 4) > 1 THEN COALESCE(mm.b103,  0) ELSE 0 END + CASE WHEN MOD(pb.pb26, 2) = 1 THEN COALESCE(mm.b104,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb27, 16)> 7 THEN COALESCE(mm.b105,  0) ELSE 0 END + CASE WHEN MOD(pb.pb27, 8) > 3 THEN COALESCE(mm.b106,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb27, 4) > 1 THEN COALESCE(mm.b107,  0) ELSE 0 END + CASE WHEN MOD(pb.pb27, 2) = 1 THEN COALESCE(mm.b108,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb28, 16)> 7 THEN COALESCE(mm.b109,  0) ELSE 0 END + CASE WHEN MOD(pb.pb28, 8) > 3 THEN COALESCE(mm.b110,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb28, 4) > 1 THEN COALESCE(mm.b111,  0) ELSE 0 END + CASE WHEN MOD(pb.pb28, 2) = 1 THEN COALESCE(mm.b112,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb29, 16)> 7 THEN COALESCE(mm.b113,  0) ELSE 0 END + CASE WHEN MOD(pb.pb29, 8) > 3 THEN COALESCE(mm.b114,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb29, 4) > 1 THEN COALESCE(mm.b115,  0) ELSE 0 END + CASE WHEN MOD(pb.pb29, 2) = 1 THEN COALESCE(mm.b116,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb30, 16)> 7 THEN COALESCE(mm.b117,  0) ELSE 0 END + CASE WHEN MOD(pb.pb30, 8) > 3 THEN COALESCE(mm.b118,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb30, 4) > 1 THEN COALESCE(mm.b119,  0) ELSE 0 END + CASE WHEN MOD(pb.pb30, 2) = 1 THEN COALESCE(mm.b120,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb31, 16)> 7 THEN COALESCE(mm.b121,  0) ELSE 0 END + CASE WHEN MOD(pb.pb31, 8) > 3 THEN COALESCE(mm.b122,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb31, 4) > 1 THEN COALESCE(mm.b123,  0) ELSE 0 END + CASE WHEN MOD(pb.pb31, 2) = 1 THEN COALESCE(mm.b124,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb32, 16)> 7 THEN COALESCE(mm.b125,  0) ELSE 0 END + CASE WHEN MOD(pb.pb32, 8) > 3 THEN COALESCE(mm.b126,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb32, 4) > 1 THEN COALESCE(mm.b127,  0) ELSE 0 END + CASE WHEN MOD(pb.pb32, 2) = 1 THEN COALESCE(mm.b128,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb33, 16)> 7 THEN COALESCE(mm.b129,  0) ELSE 0 END + CASE WHEN MOD(pb.pb33, 8) > 3 THEN COALESCE(mm.b130,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb33, 4) > 1 THEN COALESCE(mm.b131,  0) ELSE 0 END + CASE WHEN MOD(pb.pb33, 2) = 1 THEN COALESCE(mm.b132,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb34, 16)> 7 THEN COALESCE(mm.b133,  0) ELSE 0 END + CASE WHEN MOD(pb.pb34, 8) > 3 THEN COALESCE(mm.b134,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb34, 4) > 1 THEN COALESCE(mm.b135,  0) ELSE 0 END + CASE WHEN MOD(pb.pb34, 2) = 1 THEN COALESCE(mm.b136,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb35, 16)> 7 THEN COALESCE(mm.b137,  0) ELSE 0 END + CASE WHEN MOD(pb.pb35, 8) > 3 THEN COALESCE(mm.b138,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb35, 4) > 1 THEN COALESCE(mm.b139,  0) ELSE 0 END + CASE WHEN MOD(pb.pb35, 2) = 1 THEN COALESCE(mm.b140,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb36, 16)> 7 THEN COALESCE(mm.b141,  0) ELSE 0 END + CASE WHEN MOD(pb.pb36, 8) > 3 THEN COALESCE(mm.b142,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb36, 4) > 1 THEN COALESCE(mm.b143,  0) ELSE 0 END + CASE WHEN MOD(pb.pb36, 2) = 1 THEN COALESCE(mm.b144,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb37, 16)> 7 THEN COALESCE(mm.b145,  0) ELSE 0 END + CASE WHEN MOD(pb.pb37, 8) > 3 THEN COALESCE(mm.b146,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb37, 4) > 1 THEN COALESCE(mm.b147,  0) ELSE 0 END + CASE WHEN MOD(pb.pb37, 2) = 1 THEN COALESCE(mm.b148,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb38, 16)> 7 THEN COALESCE(mm.b149,  0) ELSE 0 END + CASE WHEN MOD(pb.pb38, 8) > 3 THEN COALESCE(mm.b150,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb38, 4) > 1 THEN COALESCE(mm.b151,  0) ELSE 0 END + CASE WHEN MOD(pb.pb38, 2) = 1 THEN COALESCE(mm.b152,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb39, 16)> 7 THEN COALESCE(mm.b153,  0) ELSE 0 END + CASE WHEN MOD(pb.pb39, 8) > 3 THEN COALESCE(mm.b154,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb39, 4) > 1 THEN COALESCE(mm.b155,  0) ELSE 0 END + CASE WHEN MOD(pb.pb39, 2) = 1 THEN COALESCE(mm.b156,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb40, 16)> 7 THEN COALESCE(mm.b157,  0) ELSE 0 END + CASE WHEN MOD(pb.pb40, 8) > 3 THEN COALESCE(mm.b158,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb40, 4) > 1 THEN COALESCE(mm.b159,  0) ELSE 0 END + CASE WHEN MOD(pb.pb40, 2) = 1 THEN COALESCE(mm.b160,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb41, 16)> 7 THEN COALESCE(mm.b161,  0) ELSE 0 END + CASE WHEN MOD(pb.pb41, 8) > 3 THEN COALESCE(mm.b162,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb41, 4) > 1 THEN COALESCE(mm.b163,  0) ELSE 0 END + CASE WHEN MOD(pb.pb41, 2) = 1 THEN COALESCE(mm.b164,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb42, 16)> 7 THEN COALESCE(mm.b165,  0) ELSE 0 END + CASE WHEN MOD(pb.pb42, 8) > 3 THEN COALESCE(mm.b166,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb42, 4) > 1 THEN COALESCE(mm.b167,  0) ELSE 0 END + CASE WHEN MOD(pb.pb42, 2) = 1 THEN COALESCE(mm.b168,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb43, 16)> 7 THEN COALESCE(mm.b169,  0) ELSE 0 END + CASE WHEN MOD(pb.pb43, 8) > 3 THEN COALESCE(mm.b170,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb43, 4) > 1 THEN COALESCE(mm.b171,  0) ELSE 0 END + CASE WHEN MOD(pb.pb43, 2) = 1 THEN COALESCE(mm.b172,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb44, 16)> 7 THEN COALESCE(mm.b173,  0) ELSE 0 END + CASE WHEN MOD(pb.pb44, 8) > 3 THEN COALESCE(mm.b174,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb44, 4) > 1 THEN COALESCE(mm.b175,  0) ELSE 0 END + CASE WHEN MOD(pb.pb44, 2) = 1 THEN COALESCE(mm.b176,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb45, 16)> 7 THEN COALESCE(mm.b177,  0) ELSE 0 END + CASE WHEN MOD(pb.pb45, 8) > 3 THEN COALESCE(mm.b178,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb45, 4) > 1 THEN COALESCE(mm.b179,  0) ELSE 0 END + CASE WHEN MOD(pb.pb45, 2) = 1 THEN COALESCE(mm.b180,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb46, 16)> 7 THEN COALESCE(mm.b181,  0) ELSE 0 END + CASE WHEN MOD(pb.pb46, 8) > 3 THEN COALESCE(mm.b182,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb46, 4) > 1 THEN COALESCE(mm.b183,  0) ELSE 0 END + CASE WHEN MOD(pb.pb46, 2) = 1 THEN COALESCE(mm.b184,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb47, 16)> 7 THEN COALESCE(mm.b185,  0) ELSE 0 END + CASE WHEN MOD(pb.pb47, 8) > 3 THEN COALESCE(mm.b186,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb47, 4) > 1 THEN COALESCE(mm.b187,  0) ELSE 0 END + CASE WHEN MOD(pb.pb47, 2) = 1 THEN COALESCE(mm.b188,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb48, 16)> 7 THEN COALESCE(mm.b189,  0) ELSE 0 END + CASE WHEN MOD(pb.pb48, 8) > 3 THEN COALESCE(mm.b190,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb48, 4) > 1 THEN COALESCE(mm.b191,  0) ELSE 0 END + CASE WHEN MOD(pb.pb48, 2) = 1 THEN COALESCE(mm.b192,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb49, 16)> 7 THEN COALESCE(mm.b193,  0) ELSE 0 END + CASE WHEN MOD(pb.pb49, 8) > 3 THEN COALESCE(mm.b194,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb49, 4) > 1 THEN COALESCE(mm.b195,  0) ELSE 0 END + CASE WHEN MOD(pb.pb49, 2) = 1 THEN COALESCE(mm.b196,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb50, 16)> 7 THEN COALESCE(mm.b197,  0) ELSE 0 END + CASE WHEN MOD(pb.pb50, 8) > 3 THEN COALESCE(mm.b198,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb50, 4) > 1 THEN COALESCE(mm.b199,  0) ELSE 0 END + CASE WHEN MOD(pb.pb50, 2) = 1 THEN COALESCE(mm.b200,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb51, 16)> 7 THEN COALESCE(mm.b201,  0) ELSE 0 END + CASE WHEN MOD(pb.pb51, 8) > 3 THEN COALESCE(mm.b202,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb51, 4) > 1 THEN COALESCE(mm.b203,  0) ELSE 0 END + CASE WHEN MOD(pb.pb51, 2) = 1 THEN COALESCE(mm.b204,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb52, 16)> 7 THEN COALESCE(mm.b205,  0) ELSE 0 END + CASE WHEN MOD(pb.pb52, 8) > 3 THEN COALESCE(mm.b206,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb52, 4) > 1 THEN COALESCE(mm.b207,  0) ELSE 0 END + CASE WHEN MOD(pb.pb52, 2) = 1 THEN COALESCE(mm.b208,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb53, 16)> 7 THEN COALESCE(mm.b209,  0) ELSE 0 END + CASE WHEN MOD(pb.pb53, 8) > 3 THEN COALESCE(mm.b210,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb53, 4) > 1 THEN COALESCE(mm.b211,  0) ELSE 0 END + CASE WHEN MOD(pb.pb53, 2) = 1 THEN COALESCE(mm.b212,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb54, 16)> 7 THEN COALESCE(mm.b213,  0) ELSE 0 END + CASE WHEN MOD(pb.pb54, 8) > 3 THEN COALESCE(mm.b214,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb54, 4) > 1 THEN COALESCE(mm.b215,  0) ELSE 0 END + CASE WHEN MOD(pb.pb54, 2) = 1 THEN COALESCE(mm.b216,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb55, 16)> 7 THEN COALESCE(mm.b217,  0) ELSE 0 END + CASE WHEN MOD(pb.pb55, 8) > 3 THEN COALESCE(mm.b218,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb55, 4) > 1 THEN COALESCE(mm.b219,  0) ELSE 0 END + CASE WHEN MOD(pb.pb55, 2) = 1 THEN COALESCE(mm.b220,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb56, 16)> 7 THEN COALESCE(mm.b221,  0) ELSE 0 END + CASE WHEN MOD(pb.pb56, 8) > 3 THEN COALESCE(mm.b222,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb56, 4) > 1 THEN COALESCE(mm.b223,  0) ELSE 0 END + CASE WHEN MOD(pb.pb56, 2) = 1 THEN COALESCE(mm.b224,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb57, 16)> 7 THEN COALESCE(mm.b225,  0) ELSE 0 END + CASE WHEN MOD(pb.pb57, 8) > 3 THEN COALESCE(mm.b226,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb57, 4) > 1 THEN COALESCE(mm.b227,  0) ELSE 0 END + CASE WHEN MOD(pb.pb57, 2) = 1 THEN COALESCE(mm.b228,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb58, 16)> 7 THEN COALESCE(mm.b229,  0) ELSE 0 END + CASE WHEN MOD(pb.pb58, 8) > 3 THEN COALESCE(mm.b230,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb58, 4) > 1 THEN COALESCE(mm.b231,  0) ELSE 0 END + CASE WHEN MOD(pb.pb58, 2) = 1 THEN COALESCE(mm.b232,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb59, 16)> 7 THEN COALESCE(mm.b233,  0) ELSE 0 END + CASE WHEN MOD(pb.pb59, 8) > 3 THEN COALESCE(mm.b234,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb59, 4) > 1 THEN COALESCE(mm.b235,  0) ELSE 0 END + CASE WHEN MOD(pb.pb59, 2) = 1 THEN COALESCE(mm.b236,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb60, 16)> 7 THEN COALESCE(mm.b237,  0) ELSE 0 END + CASE WHEN MOD(pb.pb60, 8) > 3 THEN COALESCE(mm.b238,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb60, 4) > 1 THEN COALESCE(mm.b239,  0) ELSE 0 END + CASE WHEN MOD(pb.pb60, 2) = 1 THEN COALESCE(mm.b240,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb61, 16)> 7 THEN COALESCE(mm.b241,  0) ELSE 0 END + CASE WHEN MOD(pb.pb61, 8) > 3 THEN COALESCE(mm.b242,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb61, 4) > 1 THEN COALESCE(mm.b243,  0) ELSE 0 END + CASE WHEN MOD(pb.pb61, 2) = 1 THEN COALESCE(mm.b244,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb62, 16)> 7 THEN COALESCE(mm.b245,  0) ELSE 0 END + CASE WHEN MOD(pb.pb62, 8) > 3 THEN COALESCE(mm.b246,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb62, 4) > 1 THEN COALESCE(mm.b247,  0) ELSE 0 END + CASE WHEN MOD(pb.pb62, 2) = 1 THEN COALESCE(mm.b248,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb63, 16)> 7 THEN COALESCE(mm.b249,  0) ELSE 0 END + CASE WHEN MOD(pb.pb63, 8) > 3 THEN COALESCE(mm.b250,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb63, 4) > 1 THEN COALESCE(mm.b251,  0) ELSE 0 END + CASE WHEN MOD(pb.pb63, 2) = 1 THEN COALESCE(mm.b252,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb64, 16)> 7 THEN COALESCE(mm.b253,  0) ELSE 0 END + CASE WHEN MOD(pb.pb64, 8) > 3 THEN COALESCE(mm.b254,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb64, 4) > 1 THEN COALESCE(mm.b255,  0) ELSE 0 END + CASE WHEN MOD(pb.pb64, 2) = 1 THEN COALESCE(mm.b256,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb65, 16)> 7 THEN COALESCE(mm.b257,  0) ELSE 0 END + CASE WHEN MOD(pb.pb65, 8) > 3 THEN COALESCE(mm.b258,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb65, 4) > 1 THEN COALESCE(mm.b259,  0) ELSE 0 END + CASE WHEN MOD(pb.pb65, 2) = 1 THEN COALESCE(mm.b260,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb66, 16)> 7 THEN COALESCE(mm.b261,  0) ELSE 0 END + CASE WHEN MOD(pb.pb66, 8) > 3 THEN COALESCE(mm.b262,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb66, 4) > 1 THEN COALESCE(mm.b263,  0) ELSE 0 END + CASE WHEN MOD(pb.pb66, 2) = 1 THEN COALESCE(mm.b264,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb67, 16)> 7 THEN COALESCE(mm.b265,  0) ELSE 0 END + CASE WHEN MOD(pb.pb67, 8) > 3 THEN COALESCE(mm.b266,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb67, 4) > 1 THEN COALESCE(mm.b267,  0) ELSE 0 END + CASE WHEN MOD(pb.pb67, 2) = 1 THEN COALESCE(mm.b268,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb68, 16)> 7 THEN COALESCE(mm.b269,  0) ELSE 0 END + CASE WHEN MOD(pb.pb68, 8) > 3 THEN COALESCE(mm.b270,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb68, 4) > 1 THEN COALESCE(mm.b271,  0) ELSE 0 END + CASE WHEN MOD(pb.pb68, 2) = 1 THEN COALESCE(mm.b272,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb69, 16)> 7 THEN COALESCE(mm.b273,  0) ELSE 0 END + CASE WHEN MOD(pb.pb69, 8) > 3 THEN COALESCE(mm.b274,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb69, 4) > 1 THEN COALESCE(mm.b275,  0) ELSE 0 END + CASE WHEN MOD(pb.pb69, 2) = 1 THEN COALESCE(mm.b276,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb70, 16)> 7 THEN COALESCE(mm.b277,  0) ELSE 0 END + CASE WHEN MOD(pb.pb70, 8) > 3 THEN COALESCE(mm.b278,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb70, 4) > 1 THEN COALESCE(mm.b279,  0) ELSE 0 END + CASE WHEN MOD(pb.pb70, 2) = 1 THEN COALESCE(mm.b280,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb71, 16)> 7 THEN COALESCE(mm.b281,  0) ELSE 0 END + CASE WHEN MOD(pb.pb71, 8) > 3 THEN COALESCE(mm.b282,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb71, 4) > 1 THEN COALESCE(mm.b283,  0) ELSE 0 END + CASE WHEN MOD(pb.pb71, 2) = 1 THEN COALESCE(mm.b284,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb72, 16)> 7 THEN COALESCE(mm.b285,  0) ELSE 0 END + CASE WHEN MOD(pb.pb72, 8) > 3 THEN COALESCE(mm.b286,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb72, 4) > 1 THEN COALESCE(mm.b287,  0) ELSE 0 END + CASE WHEN MOD(pb.pb72, 2) = 1 THEN COALESCE(mm.b288,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb73, 16)> 7 THEN COALESCE(mm.b289,  0) ELSE 0 END + CASE WHEN MOD(pb.pb73, 8) > 3 THEN COALESCE(mm.b290,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb73, 4) > 1 THEN COALESCE(mm.b291,  0) ELSE 0 END + CASE WHEN MOD(pb.pb73, 2) = 1 THEN COALESCE(mm.b292,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb74, 16)> 7 THEN COALESCE(mm.b293,  0) ELSE 0 END + CASE WHEN MOD(pb.pb74, 8) > 3 THEN COALESCE(mm.b294,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb74, 4) > 1 THEN COALESCE(mm.b295,  0) ELSE 0 END + CASE WHEN MOD(pb.pb74, 2) = 1 THEN COALESCE(mm.b296,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb75, 16)> 7 THEN COALESCE(mm.b297,  0) ELSE 0 END + CASE WHEN MOD(pb.pb75, 8) > 3 THEN COALESCE(mm.b298,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb75, 4) > 1 THEN COALESCE(mm.b299,  0) ELSE 0 END + CASE WHEN MOD(pb.pb75, 2) = 1 THEN COALESCE(mm.b300,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb76, 16)> 7 THEN COALESCE(mm.b301,  0) ELSE 0 END + CASE WHEN MOD(pb.pb76, 8) > 3 THEN COALESCE(mm.b302,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb76, 4) > 1 THEN COALESCE(mm.b303,  0) ELSE 0 END + CASE WHEN MOD(pb.pb76, 2) = 1 THEN COALESCE(mm.b304,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb77, 16)> 7 THEN COALESCE(mm.b305,  0) ELSE 0 END + CASE WHEN MOD(pb.pb77, 8) > 3 THEN COALESCE(mm.b306,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb77, 4) > 1 THEN COALESCE(mm.b307,  0) ELSE 0 END + CASE WHEN MOD(pb.pb77, 2) = 1 THEN COALESCE(mm.b308,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb78, 16)> 7 THEN COALESCE(mm.b309,  0) ELSE 0 END + CASE WHEN MOD(pb.pb78, 8) > 3 THEN COALESCE(mm.b310,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb78, 4) > 1 THEN COALESCE(mm.b311,  0) ELSE 0 END + CASE WHEN MOD(pb.pb78, 2) = 1 THEN COALESCE(mm.b312,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb79, 16)> 7 THEN COALESCE(mm.b313,  0) ELSE 0 END + CASE WHEN MOD(pb.pb79, 8) > 3 THEN COALESCE(mm.b314,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb79, 4) > 1 THEN COALESCE(mm.b315,  0) ELSE 0 END + CASE WHEN MOD(pb.pb79, 2) = 1 THEN COALESCE(mm.b316,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb80, 16)> 7 THEN COALESCE(mm.b317,  0) ELSE 0 END + CASE WHEN MOD(pb.pb80, 8) > 3 THEN COALESCE(mm.b318,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb80, 4) > 1 THEN COALESCE(mm.b319,  0) ELSE 0 END + CASE WHEN MOD(pb.pb80, 2) = 1 THEN COALESCE(mm.b320,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb81, 16)> 7 THEN COALESCE(mm.b321,  0) ELSE 0 END + CASE WHEN MOD(pb.pb81, 8) > 3 THEN COALESCE(mm.b322,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb81, 4) > 1 THEN COALESCE(mm.b323,  0) ELSE 0 END + CASE WHEN MOD(pb.pb81, 2) = 1 THEN COALESCE(mm.b324,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb82, 16)> 7 THEN COALESCE(mm.b325,  0) ELSE 0 END + CASE WHEN MOD(pb.pb82, 8) > 3 THEN COALESCE(mm.b326,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb82, 4) > 1 THEN COALESCE(mm.b327,  0) ELSE 0 END + CASE WHEN MOD(pb.pb82, 2) = 1 THEN COALESCE(mm.b328,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb83, 16)> 7 THEN COALESCE(mm.b329,  0) ELSE 0 END + CASE WHEN MOD(pb.pb83, 8) > 3 THEN COALESCE(mm.b330,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb83, 4) > 1 THEN COALESCE(mm.b331,  0) ELSE 0 END + CASE WHEN MOD(pb.pb83, 2) = 1 THEN COALESCE(mm.b332,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb84, 16)> 7 THEN COALESCE(mm.b333,  0) ELSE 0 END + CASE WHEN MOD(pb.pb84, 8) > 3 THEN COALESCE(mm.b334,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb84, 4) > 1 THEN COALESCE(mm.b335,  0) ELSE 0 END + CASE WHEN MOD(pb.pb84, 2) = 1 THEN COALESCE(mm.b336,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb85, 16)> 7 THEN COALESCE(mm.b337,  0) ELSE 0 END + CASE WHEN MOD(pb.pb85, 8) > 3 THEN COALESCE(mm.b338,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb85, 4) > 1 THEN COALESCE(mm.b339,  0) ELSE 0 END + CASE WHEN MOD(pb.pb85, 2) = 1 THEN COALESCE(mm.b340,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb86, 16)> 7 THEN COALESCE(mm.b341,  0) ELSE 0 END + CASE WHEN MOD(pb.pb86, 8) > 3 THEN COALESCE(mm.b342,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb86, 4) > 1 THEN COALESCE(mm.b343,  0) ELSE 0 END + CASE WHEN MOD(pb.pb86, 2) = 1 THEN COALESCE(mm.b344,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb87, 16)> 7 THEN COALESCE(mm.b345,  0) ELSE 0 END + CASE WHEN MOD(pb.pb87, 8) > 3 THEN COALESCE(mm.b346,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb87, 4) > 1 THEN COALESCE(mm.b347,  0) ELSE 0 END + CASE WHEN MOD(pb.pb87, 2) = 1 THEN COALESCE(mm.b348,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb88, 16)> 7 THEN COALESCE(mm.b349,  0) ELSE 0 END + CASE WHEN MOD(pb.pb88, 8) > 3 THEN COALESCE(mm.b350,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb88, 4) > 1 THEN COALESCE(mm.b351,  0) ELSE 0 END + CASE WHEN MOD(pb.pb88, 2) = 1 THEN COALESCE(mm.b352,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb89, 16)> 7 THEN COALESCE(mm.b353,  0) ELSE 0 END + CASE WHEN MOD(pb.pb89, 8) > 3 THEN COALESCE(mm.b354,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb89, 4) > 1 THEN COALESCE(mm.b355,  0) ELSE 0 END + CASE WHEN MOD(pb.pb89, 2) = 1 THEN COALESCE(mm.b356,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb90, 16)> 7 THEN COALESCE(mm.b357,  0) ELSE 0 END + CASE WHEN MOD(pb.pb90, 8) > 3 THEN COALESCE(mm.b358,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb90, 4) > 1 THEN COALESCE(mm.b359,  0) ELSE 0 END + CASE WHEN MOD(pb.pb90, 2) = 1 THEN COALESCE(mm.b360,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb91, 16)> 7 THEN COALESCE(mm.b361,  0) ELSE 0 END + CASE WHEN MOD(pb.pb91, 8) > 3 THEN COALESCE(mm.b362,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb91, 4) > 1 THEN COALESCE(mm.b363,  0) ELSE 0 END + CASE WHEN MOD(pb.pb91, 2) = 1 THEN COALESCE(mm.b364,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb92, 16)> 7 THEN COALESCE(mm.b365,  0) ELSE 0 END + CASE WHEN MOD(pb.pb92, 8) > 3 THEN COALESCE(mm.b366,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb92, 4) > 1 THEN COALESCE(mm.b367,  0) ELSE 0 END + CASE WHEN MOD(pb.pb92, 2) = 1 THEN COALESCE(mm.b368,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb93, 16)> 7 THEN COALESCE(mm.b369,  0) ELSE 0 END + CASE WHEN MOD(pb.pb93, 8) > 3 THEN COALESCE(mm.b370,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb93, 4) > 1 THEN COALESCE(mm.b371,  0) ELSE 0 END + CASE WHEN MOD(pb.pb93, 2) = 1 THEN COALESCE(mm.b372,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb94, 16)> 7 THEN COALESCE(mm.b373,  0) ELSE 0 END + CASE WHEN MOD(pb.pb94, 8) > 3 THEN COALESCE(mm.b374,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb94, 4) > 1 THEN COALESCE(mm.b375,  0) ELSE 0 END + CASE WHEN MOD(pb.pb94, 2) = 1 THEN COALESCE(mm.b376,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb95, 16)> 7 THEN COALESCE(mm.b377,  0) ELSE 0 END + CASE WHEN MOD(pb.pb95, 8) > 3 THEN COALESCE(mm.b378,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb95, 4) > 1 THEN COALESCE(mm.b379,  0) ELSE 0 END + CASE WHEN MOD(pb.pb95, 2) = 1 THEN COALESCE(mm.b380,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb96, 16)> 7 THEN COALESCE(mm.b381,  0) ELSE 0 END + CASE WHEN MOD(pb.pb96, 8) > 3 THEN COALESCE(mm.b382,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb96, 4) > 1 THEN COALESCE(mm.b383,  0) ELSE 0 END + CASE WHEN MOD(pb.pb96, 2) = 1 THEN COALESCE(mm.b384,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb97, 16)> 7 THEN COALESCE(mm.b385,  0) ELSE 0 END + CASE WHEN MOD(pb.pb97, 8) > 3 THEN COALESCE(mm.b386,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb97, 4) > 1 THEN COALESCE(mm.b387,  0) ELSE 0 END + CASE WHEN MOD(pb.pb97, 2) = 1 THEN COALESCE(mm.b388,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb98, 16)> 7 THEN COALESCE(mm.b389,  0) ELSE 0 END + CASE WHEN MOD(pb.pb98, 8) > 3 THEN COALESCE(mm.b390,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb98, 4) > 1 THEN COALESCE(mm.b391,  0) ELSE 0 END + CASE WHEN MOD(pb.pb98, 2) = 1 THEN COALESCE(mm.b392,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb99, 16)> 7 THEN COALESCE(mm.b393,  0) ELSE 0 END + CASE WHEN MOD(pb.pb99, 8) > 3 THEN COALESCE(mm.b394,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb99, 4) > 1 THEN COALESCE(mm.b395,  0) ELSE 0 END + CASE WHEN MOD(pb.pb99, 2) = 1 THEN COALESCE(mm.b396,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb100, 16)> 7 THEN COALESCE(mm.b397,  0) ELSE 0 END + CASE WHEN MOD(pb.pb100, 8) > 3 THEN COALESCE(mm.b398,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb100, 4) > 1 THEN COALESCE(mm.b399,  0) ELSE 0 END + CASE WHEN MOD(pb.pb100, 2) = 1 THEN COALESCE(mm.b400,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb101, 16)> 7 THEN COALESCE(mm.b401,  0) ELSE 0 END + CASE WHEN MOD(pb.pb101, 8) > 3 THEN COALESCE(mm.b402,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb101, 4) > 1 THEN COALESCE(mm.b403,  0) ELSE 0 END + CASE WHEN MOD(pb.pb101, 2) = 1 THEN COALESCE(mm.b404,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb102, 16)> 7 THEN COALESCE(mm.b405,  0) ELSE 0 END + CASE WHEN MOD(pb.pb102, 8) > 3 THEN COALESCE(mm.b406,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb102, 4) > 1 THEN COALESCE(mm.b407,  0) ELSE 0 END + CASE WHEN MOD(pb.pb102, 2) = 1 THEN COALESCE(mm.b408,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb103, 16)> 7 THEN COALESCE(mm.b409,  0) ELSE 0 END + CASE WHEN MOD(pb.pb103, 8) > 3 THEN COALESCE(mm.b410,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb103, 4) > 1 THEN COALESCE(mm.b411,  0) ELSE 0 END + CASE WHEN MOD(pb.pb103, 2) = 1 THEN COALESCE(mm.b412,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb104, 16)> 7 THEN COALESCE(mm.b413,  0) ELSE 0 END + CASE WHEN MOD(pb.pb104, 8) > 3 THEN COALESCE(mm.b414,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb104, 4) > 1 THEN COALESCE(mm.b415,  0) ELSE 0 END + CASE WHEN MOD(pb.pb104, 2) = 1 THEN COALESCE(mm.b416,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb105, 16)> 7 THEN COALESCE(mm.b417,  0) ELSE 0 END + CASE WHEN MOD(pb.pb105, 8) > 3 THEN COALESCE(mm.b418,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb105, 4) > 1 THEN COALESCE(mm.b419,  0) ELSE 0 END + CASE WHEN MOD(pb.pb105, 2) = 1 THEN COALESCE(mm.b420,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb106, 16)> 7 THEN COALESCE(mm.b421,  0) ELSE 0 END + CASE WHEN MOD(pb.pb106, 8) > 3 THEN COALESCE(mm.b422,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb106, 4) > 1 THEN COALESCE(mm.b423,  0) ELSE 0 END + CASE WHEN MOD(pb.pb106, 2) = 1 THEN COALESCE(mm.b424,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb107, 16)> 7 THEN COALESCE(mm.b425,  0) ELSE 0 END + CASE WHEN MOD(pb.pb107, 8) > 3 THEN COALESCE(mm.b426,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb107, 4) > 1 THEN COALESCE(mm.b427,  0) ELSE 0 END + CASE WHEN MOD(pb.pb107, 2) = 1 THEN COALESCE(mm.b428,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb108, 16)> 7 THEN COALESCE(mm.b429,  0) ELSE 0 END + CASE WHEN MOD(pb.pb108, 8) > 3 THEN COALESCE(mm.b430,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb108, 4) > 1 THEN COALESCE(mm.b431,  0) ELSE 0 END + CASE WHEN MOD(pb.pb108, 2) = 1 THEN COALESCE(mm.b432,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb109, 16)> 7 THEN COALESCE(mm.b433,  0) ELSE 0 END + CASE WHEN MOD(pb.pb109, 8) > 3 THEN COALESCE(mm.b434,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb109, 4) > 1 THEN COALESCE(mm.b435,  0) ELSE 0 END + CASE WHEN MOD(pb.pb109, 2) = 1 THEN COALESCE(mm.b436,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb110, 16)> 7 THEN COALESCE(mm.b437,  0) ELSE 0 END + CASE WHEN MOD(pb.pb110, 8) > 3 THEN COALESCE(mm.b438,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb110, 4) > 1 THEN COALESCE(mm.b439,  0) ELSE 0 END + CASE WHEN MOD(pb.pb110, 2) = 1 THEN COALESCE(mm.b440,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb111, 16)> 7 THEN COALESCE(mm.b441,  0) ELSE 0 END + CASE WHEN MOD(pb.pb111, 8) > 3 THEN COALESCE(mm.b442,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb111, 4) > 1 THEN COALESCE(mm.b443,  0) ELSE 0 END + CASE WHEN MOD(pb.pb111, 2) = 1 THEN COALESCE(mm.b444,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb112, 16)> 7 THEN COALESCE(mm.b445,  0) ELSE 0 END + CASE WHEN MOD(pb.pb112, 8) > 3 THEN COALESCE(mm.b446,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb112, 4) > 1 THEN COALESCE(mm.b447,  0) ELSE 0 END + CASE WHEN MOD(pb.pb112, 2) = 1 THEN COALESCE(mm.b448,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb113, 16)> 7 THEN COALESCE(mm.b449,  0) ELSE 0 END + CASE WHEN MOD(pb.pb113, 8) > 3 THEN COALESCE(mm.b450,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb113, 4) > 1 THEN COALESCE(mm.b451,  0) ELSE 0 END + CASE WHEN MOD(pb.pb113, 2) = 1 THEN COALESCE(mm.b452,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb114, 16)> 7 THEN COALESCE(mm.b453,  0) ELSE 0 END + CASE WHEN MOD(pb.pb114, 8) > 3 THEN COALESCE(mm.b454,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb114, 4) > 1 THEN COALESCE(mm.b455,  0) ELSE 0 END + CASE WHEN MOD(pb.pb114, 2) = 1 THEN COALESCE(mm.b456,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb115, 16)> 7 THEN COALESCE(mm.b457,  0) ELSE 0 END + CASE WHEN MOD(pb.pb115, 8) > 3 THEN COALESCE(mm.b458,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb115, 4) > 1 THEN COALESCE(mm.b459,  0) ELSE 0 END + CASE WHEN MOD(pb.pb115, 2) = 1 THEN COALESCE(mm.b460,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb116, 16)> 7 THEN COALESCE(mm.b461,  0) ELSE 0 END + CASE WHEN MOD(pb.pb116, 8) > 3 THEN COALESCE(mm.b462,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb116, 4) > 1 THEN COALESCE(mm.b463,  0) ELSE 0 END + CASE WHEN MOD(pb.pb116, 2) = 1 THEN COALESCE(mm.b464,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb117, 16)> 7 THEN COALESCE(mm.b465,  0) ELSE 0 END + CASE WHEN MOD(pb.pb117, 8) > 3 THEN COALESCE(mm.b466,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb117, 4) > 1 THEN COALESCE(mm.b467,  0) ELSE 0 END + CASE WHEN MOD(pb.pb117, 2) = 1 THEN COALESCE(mm.b468,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb118, 16)> 7 THEN COALESCE(mm.b469,  0) ELSE 0 END + CASE WHEN MOD(pb.pb118, 8) > 3 THEN COALESCE(mm.b470,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb118, 4) > 1 THEN COALESCE(mm.b471,  0) ELSE 0 END + CASE WHEN MOD(pb.pb118, 2) = 1 THEN COALESCE(mm.b472,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb119, 16)> 7 THEN COALESCE(mm.b473,  0) ELSE 0 END + CASE WHEN MOD(pb.pb119, 8) > 3 THEN COALESCE(mm.b474,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb119, 4) > 1 THEN COALESCE(mm.b475,  0) ELSE 0 END + CASE WHEN MOD(pb.pb119, 2) = 1 THEN COALESCE(mm.b476,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb120, 16)> 7 THEN COALESCE(mm.b477,  0) ELSE 0 END + CASE WHEN MOD(pb.pb120, 8) > 3 THEN COALESCE(mm.b478,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb120, 4) > 1 THEN COALESCE(mm.b479,  0) ELSE 0 END + CASE WHEN MOD(pb.pb120, 2) = 1 THEN COALESCE(mm.b480,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb121, 16)> 7 THEN COALESCE(mm.b481,  0) ELSE 0 END + CASE WHEN MOD(pb.pb121, 8) > 3 THEN COALESCE(mm.b482,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb121, 4) > 1 THEN COALESCE(mm.b483,  0) ELSE 0 END + CASE WHEN MOD(pb.pb121, 2) = 1 THEN COALESCE(mm.b484,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb122, 16)> 7 THEN COALESCE(mm.b485,  0) ELSE 0 END + CASE WHEN MOD(pb.pb122, 8) > 3 THEN COALESCE(mm.b486,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb122, 4) > 1 THEN COALESCE(mm.b487,  0) ELSE 0 END + CASE WHEN MOD(pb.pb122, 2) = 1 THEN COALESCE(mm.b488,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb123, 16)> 7 THEN COALESCE(mm.b489,  0) ELSE 0 END + CASE WHEN MOD(pb.pb123, 8) > 3 THEN COALESCE(mm.b490,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb123, 4) > 1 THEN COALESCE(mm.b491,  0) ELSE 0 END + CASE WHEN MOD(pb.pb123, 2) = 1 THEN COALESCE(mm.b492,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb124, 16)> 7 THEN COALESCE(mm.b493,  0) ELSE 0 END + CASE WHEN MOD(pb.pb124, 8) > 3 THEN COALESCE(mm.b494,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb124, 4) > 1 THEN COALESCE(mm.b495,  0) ELSE 0 END + CASE WHEN MOD(pb.pb124, 2) = 1 THEN COALESCE(mm.b496,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb125, 16)> 7 THEN COALESCE(mm.b497,  0) ELSE 0 END + CASE WHEN MOD(pb.pb125, 8) > 3 THEN COALESCE(mm.b498,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb125, 4) > 1 THEN COALESCE(mm.b499,  0) ELSE 0 END + CASE WHEN MOD(pb.pb125, 2) = 1 THEN COALESCE(mm.b500,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb126, 16)> 7 THEN COALESCE(mm.b501,  0) ELSE 0 END + CASE WHEN MOD(pb.pb126, 8) > 3 THEN COALESCE(mm.b502,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb126, 4) > 1 THEN COALESCE(mm.b503,  0) ELSE 0 END + CASE WHEN MOD(pb.pb126, 2) = 1 THEN COALESCE(mm.b504,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb127, 16)> 7 THEN COALESCE(mm.b505,  0) ELSE 0 END + CASE WHEN MOD(pb.pb127, 8) > 3 THEN COALESCE(mm.b506,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb127, 4) > 1 THEN COALESCE(mm.b507,  0) ELSE 0 END + CASE WHEN MOD(pb.pb127, 2) = 1 THEN COALESCE(mm.b508,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb128, 16)> 7 THEN COALESCE(mm.b509,  0) ELSE 0 END + CASE WHEN MOD(pb.pb128, 8) > 3 THEN COALESCE(mm.b510,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb128, 4) > 1 THEN COALESCE(mm.b511,  0) ELSE 0 END + CASE WHEN MOD(pb.pb128, 2) = 1 THEN COALESCE(mm.b512,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb129, 16)> 7 THEN COALESCE(mm.b513,  0) ELSE 0 END + CASE WHEN MOD(pb.pb129, 8) > 3 THEN COALESCE(mm.b514,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb129, 4) > 1 THEN COALESCE(mm.b515,  0) ELSE 0 END + CASE WHEN MOD(pb.pb129, 2) = 1 THEN COALESCE(mm.b516,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb130, 16)> 7 THEN COALESCE(mm.b517,  0) ELSE 0 END + CASE WHEN MOD(pb.pb130, 8) > 3 THEN COALESCE(mm.b518,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb130, 4) > 1 THEN COALESCE(mm.b519,  0) ELSE 0 END + CASE WHEN MOD(pb.pb130, 2) = 1 THEN COALESCE(mm.b520,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb131, 16)> 7 THEN COALESCE(mm.b521,  0) ELSE 0 END + CASE WHEN MOD(pb.pb131, 8) > 3 THEN COALESCE(mm.b522,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb131, 4) > 1 THEN COALESCE(mm.b523,  0) ELSE 0 END + CASE WHEN MOD(pb.pb131, 2) = 1 THEN COALESCE(mm.b524,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb132, 16)> 7 THEN COALESCE(mm.b525,  0) ELSE 0 END + CASE WHEN MOD(pb.pb132, 8) > 3 THEN COALESCE(mm.b526,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb132, 4) > 1 THEN COALESCE(mm.b527,  0) ELSE 0 END + CASE WHEN MOD(pb.pb132, 2) = 1 THEN COALESCE(mm.b528,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb133, 16)> 7 THEN COALESCE(mm.b529,  0) ELSE 0 END + CASE WHEN MOD(pb.pb133, 8) > 3 THEN COALESCE(mm.b530,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb133, 4) > 1 THEN COALESCE(mm.b531,  0) ELSE 0 END + CASE WHEN MOD(pb.pb133, 2) = 1 THEN COALESCE(mm.b532,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb134, 16)> 7 THEN COALESCE(mm.b533,  0) ELSE 0 END + CASE WHEN MOD(pb.pb134, 8) > 3 THEN COALESCE(mm.b534,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb134, 4) > 1 THEN COALESCE(mm.b535,  0) ELSE 0 END + CASE WHEN MOD(pb.pb134, 2) = 1 THEN COALESCE(mm.b536,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb135, 16)> 7 THEN COALESCE(mm.b537,  0) ELSE 0 END + CASE WHEN MOD(pb.pb135, 8) > 3 THEN COALESCE(mm.b538,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb135, 4) > 1 THEN COALESCE(mm.b539,  0) ELSE 0 END + CASE WHEN MOD(pb.pb135, 2) = 1 THEN COALESCE(mm.b540,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb136, 16)> 7 THEN COALESCE(mm.b541,  0) ELSE 0 END + CASE WHEN MOD(pb.pb136, 8) > 3 THEN COALESCE(mm.b542,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb136, 4) > 1 THEN COALESCE(mm.b543,  0) ELSE 0 END + CASE WHEN MOD(pb.pb136, 2) = 1 THEN COALESCE(mm.b544,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb137, 16)> 7 THEN COALESCE(mm.b545,  0) ELSE 0 END + CASE WHEN MOD(pb.pb137, 8) > 3 THEN COALESCE(mm.b546,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb137, 4) > 1 THEN COALESCE(mm.b547,  0) ELSE 0 END + CASE WHEN MOD(pb.pb137, 2) = 1 THEN COALESCE(mm.b548,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb138, 16)> 7 THEN COALESCE(mm.b549,  0) ELSE 0 END + CASE WHEN MOD(pb.pb138, 8) > 3 THEN COALESCE(mm.b550,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb138, 4) > 1 THEN COALESCE(mm.b551,  0) ELSE 0 END + CASE WHEN MOD(pb.pb138, 2) = 1 THEN COALESCE(mm.b552,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb139, 16)> 7 THEN COALESCE(mm.b553,  0) ELSE 0 END + CASE WHEN MOD(pb.pb139, 8) > 3 THEN COALESCE(mm.b554,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb139, 4) > 1 THEN COALESCE(mm.b555,  0) ELSE 0 END + CASE WHEN MOD(pb.pb139, 2) = 1 THEN COALESCE(mm.b556,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb140, 16)> 7 THEN COALESCE(mm.b557,  0) ELSE 0 END + CASE WHEN MOD(pb.pb140, 8) > 3 THEN COALESCE(mm.b558,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb140, 4) > 1 THEN COALESCE(mm.b559,  0) ELSE 0 END + CASE WHEN MOD(pb.pb140, 2) = 1 THEN COALESCE(mm.b560,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb141, 16)> 7 THEN COALESCE(mm.b561,  0) ELSE 0 END + CASE WHEN MOD(pb.pb141, 8) > 3 THEN COALESCE(mm.b562,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb141, 4) > 1 THEN COALESCE(mm.b563,  0) ELSE 0 END + CASE WHEN MOD(pb.pb141, 2) = 1 THEN COALESCE(mm.b564,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb142, 16)> 7 THEN COALESCE(mm.b565,  0) ELSE 0 END + CASE WHEN MOD(pb.pb142, 8) > 3 THEN COALESCE(mm.b566,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb142, 4) > 1 THEN COALESCE(mm.b567,  0) ELSE 0 END + CASE WHEN MOD(pb.pb142, 2) = 1 THEN COALESCE(mm.b568,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb143, 16)> 7 THEN COALESCE(mm.b569,  0) ELSE 0 END + CASE WHEN MOD(pb.pb143, 8) > 3 THEN COALESCE(mm.b570,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb143, 4) > 1 THEN COALESCE(mm.b571,  0) ELSE 0 END + CASE WHEN MOD(pb.pb143, 2) = 1 THEN COALESCE(mm.b572,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb144, 16)> 7 THEN COALESCE(mm.b573,  0) ELSE 0 END + CASE WHEN MOD(pb.pb144, 8) > 3 THEN COALESCE(mm.b574,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb144, 4) > 1 THEN COALESCE(mm.b575,  0) ELSE 0 END + CASE WHEN MOD(pb.pb144, 2) = 1 THEN COALESCE(mm.b576,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb145, 16)> 7 THEN COALESCE(mm.b577,  0) ELSE 0 END + CASE WHEN MOD(pb.pb145, 8) > 3 THEN COALESCE(mm.b578,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb145, 4) > 1 THEN COALESCE(mm.b579,  0) ELSE 0 END + CASE WHEN MOD(pb.pb145, 2) = 1 THEN COALESCE(mm.b580,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb146, 16)> 7 THEN COALESCE(mm.b581,  0) ELSE 0 END + CASE WHEN MOD(pb.pb146, 8) > 3 THEN COALESCE(mm.b582,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb146, 4) > 1 THEN COALESCE(mm.b583,  0) ELSE 0 END + CASE WHEN MOD(pb.pb146, 2) = 1 THEN COALESCE(mm.b584,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb147, 16)> 7 THEN COALESCE(mm.b585,  0) ELSE 0 END + CASE WHEN MOD(pb.pb147, 8) > 3 THEN COALESCE(mm.b586,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb147, 4) > 1 THEN COALESCE(mm.b587,  0) ELSE 0 END + CASE WHEN MOD(pb.pb147, 2) = 1 THEN COALESCE(mm.b588,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb148, 16)> 7 THEN COALESCE(mm.b589,  0) ELSE 0 END + CASE WHEN MOD(pb.pb148, 8) > 3 THEN COALESCE(mm.b590,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb148, 4) > 1 THEN COALESCE(mm.b591,  0) ELSE 0 END + CASE WHEN MOD(pb.pb148, 2) = 1 THEN COALESCE(mm.b592,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb149, 16)> 7 THEN COALESCE(mm.b593,  0) ELSE 0 END + CASE WHEN MOD(pb.pb149, 8) > 3 THEN COALESCE(mm.b594,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb149, 4) > 1 THEN COALESCE(mm.b595,  0) ELSE 0 END + CASE WHEN MOD(pb.pb149, 2) = 1 THEN COALESCE(mm.b596,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb150, 16)> 7 THEN COALESCE(mm.b597,  0) ELSE 0 END + CASE WHEN MOD(pb.pb150, 8) > 3 THEN COALESCE(mm.b598,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb150, 4) > 1 THEN COALESCE(mm.b599,  0) ELSE 0 END + CASE WHEN MOD(pb.pb150, 2) = 1 THEN COALESCE(mm.b600,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb151, 16)> 7 THEN COALESCE(mm.b601,  0) ELSE 0 END + CASE WHEN MOD(pb.pb151, 8) > 3 THEN COALESCE(mm.b602,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb151, 4) > 1 THEN COALESCE(mm.b603,  0) ELSE 0 END + CASE WHEN MOD(pb.pb151, 2) = 1 THEN COALESCE(mm.b604,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb152, 16)> 7 THEN COALESCE(mm.b605,  0) ELSE 0 END + CASE WHEN MOD(pb.pb152, 8) > 3 THEN COALESCE(mm.b606,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb152, 4) > 1 THEN COALESCE(mm.b607,  0) ELSE 0 END + CASE WHEN MOD(pb.pb152, 2) = 1 THEN COALESCE(mm.b608,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb153, 16)> 7 THEN COALESCE(mm.b609,  0) ELSE 0 END + CASE WHEN MOD(pb.pb153, 8) > 3 THEN COALESCE(mm.b610,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb153, 4) > 1 THEN COALESCE(mm.b611,  0) ELSE 0 END + CASE WHEN MOD(pb.pb153, 2) = 1 THEN COALESCE(mm.b612,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb154, 16)> 7 THEN COALESCE(mm.b613,  0) ELSE 0 END + CASE WHEN MOD(pb.pb154, 8) > 3 THEN COALESCE(mm.b614,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb154, 4) > 1 THEN COALESCE(mm.b615,  0) ELSE 0 END + CASE WHEN MOD(pb.pb154, 2) = 1 THEN COALESCE(mm.b616,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb155, 16)> 7 THEN COALESCE(mm.b617,  0) ELSE 0 END + CASE WHEN MOD(pb.pb155, 8) > 3 THEN COALESCE(mm.b618,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb155, 4) > 1 THEN COALESCE(mm.b619,  0) ELSE 0 END + CASE WHEN MOD(pb.pb155, 2) = 1 THEN COALESCE(mm.b620,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb156, 16)> 7 THEN COALESCE(mm.b621,  0) ELSE 0 END + CASE WHEN MOD(pb.pb156, 8) > 3 THEN COALESCE(mm.b622,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb156, 4) > 1 THEN COALESCE(mm.b623,  0) ELSE 0 END + CASE WHEN MOD(pb.pb156, 2) = 1 THEN COALESCE(mm.b624,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb157, 16)> 7 THEN COALESCE(mm.b625,  0) ELSE 0 END + CASE WHEN MOD(pb.pb157, 8) > 3 THEN COALESCE(mm.b626,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb157, 4) > 1 THEN COALESCE(mm.b627,  0) ELSE 0 END + CASE WHEN MOD(pb.pb157, 2) = 1 THEN COALESCE(mm.b628,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb158, 16)> 7 THEN COALESCE(mm.b629,  0) ELSE 0 END + CASE WHEN MOD(pb.pb158, 8) > 3 THEN COALESCE(mm.b630,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb158, 4) > 1 THEN COALESCE(mm.b631,  0) ELSE 0 END + CASE WHEN MOD(pb.pb158, 2) = 1 THEN COALESCE(mm.b632,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb159, 16)> 7 THEN COALESCE(mm.b633,  0) ELSE 0 END + CASE WHEN MOD(pb.pb159, 8) > 3 THEN COALESCE(mm.b634,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb159, 4) > 1 THEN COALESCE(mm.b635,  0) ELSE 0 END + CASE WHEN MOD(pb.pb159, 2) = 1 THEN COALESCE(mm.b636,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb160, 16)> 7 THEN COALESCE(mm.b637,  0) ELSE 0 END + CASE WHEN MOD(pb.pb160, 8) > 3 THEN COALESCE(mm.b638,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb160, 4) > 1 THEN COALESCE(mm.b639,  0) ELSE 0 END + CASE WHEN MOD(pb.pb160, 2) = 1 THEN COALESCE(mm.b640,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb161, 16)> 7 THEN COALESCE(mm.b641,  0) ELSE 0 END + CASE WHEN MOD(pb.pb161, 8) > 3 THEN COALESCE(mm.b642,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb161, 4) > 1 THEN COALESCE(mm.b643,  0) ELSE 0 END + CASE WHEN MOD(pb.pb161, 2) = 1 THEN COALESCE(mm.b644,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb162, 16)> 7 THEN COALESCE(mm.b645,  0) ELSE 0 END + CASE WHEN MOD(pb.pb162, 8) > 3 THEN COALESCE(mm.b646,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb162, 4) > 1 THEN COALESCE(mm.b647,  0) ELSE 0 END + CASE WHEN MOD(pb.pb162, 2) = 1 THEN COALESCE(mm.b648,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb163, 16)> 7 THEN COALESCE(mm.b649,  0) ELSE 0 END + CASE WHEN MOD(pb.pb163, 8) > 3 THEN COALESCE(mm.b650,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb163, 4) > 1 THEN COALESCE(mm.b651,  0) ELSE 0 END + CASE WHEN MOD(pb.pb163, 2) = 1 THEN COALESCE(mm.b652,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb164, 16)> 7 THEN COALESCE(mm.b653,  0) ELSE 0 END + CASE WHEN MOD(pb.pb164, 8) > 3 THEN COALESCE(mm.b654,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb164, 4) > 1 THEN COALESCE(mm.b655,  0) ELSE 0 END + CASE WHEN MOD(pb.pb164, 2) = 1 THEN COALESCE(mm.b656,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb165, 16)> 7 THEN COALESCE(mm.b657,  0) ELSE 0 END + CASE WHEN MOD(pb.pb165, 8) > 3 THEN COALESCE(mm.b658,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb165, 4) > 1 THEN COALESCE(mm.b659,  0) ELSE 0 END + CASE WHEN MOD(pb.pb165, 2) = 1 THEN COALESCE(mm.b660,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb166, 16)> 7 THEN COALESCE(mm.b661,  0) ELSE 0 END + CASE WHEN MOD(pb.pb166, 8) > 3 THEN COALESCE(mm.b662,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb166, 4) > 1 THEN COALESCE(mm.b663,  0) ELSE 0 END + CASE WHEN MOD(pb.pb166, 2) = 1 THEN COALESCE(mm.b664,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb167, 16)> 7 THEN COALESCE(mm.b665,  0) ELSE 0 END + CASE WHEN MOD(pb.pb167, 8) > 3 THEN COALESCE(mm.b666,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb167, 4) > 1 THEN COALESCE(mm.b667,  0) ELSE 0 END + CASE WHEN MOD(pb.pb167, 2) = 1 THEN COALESCE(mm.b668,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb168, 16)> 7 THEN COALESCE(mm.b669,  0) ELSE 0 END + CASE WHEN MOD(pb.pb168, 8) > 3 THEN COALESCE(mm.b670,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb168, 4) > 1 THEN COALESCE(mm.b671,  0) ELSE 0 END + CASE WHEN MOD(pb.pb168, 2) = 1 THEN COALESCE(mm.b672,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb169, 16)> 7 THEN COALESCE(mm.b673,  0) ELSE 0 END + CASE WHEN MOD(pb.pb169, 8) > 3 THEN COALESCE(mm.b674,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb169, 4) > 1 THEN COALESCE(mm.b675,  0) ELSE 0 END + CASE WHEN MOD(pb.pb169, 2) = 1 THEN COALESCE(mm.b676,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb170, 16)> 7 THEN COALESCE(mm.b677,  0) ELSE 0 END + CASE WHEN MOD(pb.pb170, 8) > 3 THEN COALESCE(mm.b678,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb170, 4) > 1 THEN COALESCE(mm.b679,  0) ELSE 0 END + CASE WHEN MOD(pb.pb170, 2) = 1 THEN COALESCE(mm.b680,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb171, 16)> 7 THEN COALESCE(mm.b681,  0) ELSE 0 END + CASE WHEN MOD(pb.pb171, 8) > 3 THEN COALESCE(mm.b682,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb171, 4) > 1 THEN COALESCE(mm.b683,  0) ELSE 0 END + CASE WHEN MOD(pb.pb171, 2) = 1 THEN COALESCE(mm.b684,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb172, 16)> 7 THEN COALESCE(mm.b685,  0) ELSE 0 END + CASE WHEN MOD(pb.pb172, 8) > 3 THEN COALESCE(mm.b686,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb172, 4) > 1 THEN COALESCE(mm.b687,  0) ELSE 0 END + CASE WHEN MOD(pb.pb172, 2) = 1 THEN COALESCE(mm.b688,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb173, 16)> 7 THEN COALESCE(mm.b689,  0) ELSE 0 END + CASE WHEN MOD(pb.pb173, 8) > 3 THEN COALESCE(mm.b690,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb173, 4) > 1 THEN COALESCE(mm.b691,  0) ELSE 0 END + CASE WHEN MOD(pb.pb173, 2) = 1 THEN COALESCE(mm.b692,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb174, 16)> 7 THEN COALESCE(mm.b693,  0) ELSE 0 END + CASE WHEN MOD(pb.pb174, 8) > 3 THEN COALESCE(mm.b694,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb174, 4) > 1 THEN COALESCE(mm.b695,  0) ELSE 0 END + CASE WHEN MOD(pb.pb174, 2) = 1 THEN COALESCE(mm.b696,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb175, 16)> 7 THEN COALESCE(mm.b697,  0) ELSE 0 END + CASE WHEN MOD(pb.pb175, 8) > 3 THEN COALESCE(mm.b698,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb175, 4) > 1 THEN COALESCE(mm.b699,  0) ELSE 0 END + CASE WHEN MOD(pb.pb175, 2) = 1 THEN COALESCE(mm.b700,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb176, 16)> 7 THEN COALESCE(mm.b701,  0) ELSE 0 END + CASE WHEN MOD(pb.pb176, 8) > 3 THEN COALESCE(mm.b702,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb176, 4) > 1 THEN COALESCE(mm.b703,  0) ELSE 0 END + CASE WHEN MOD(pb.pb176, 2) = 1 THEN COALESCE(mm.b704,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb177, 16)> 7 THEN COALESCE(mm.b705,  0) ELSE 0 END + CASE WHEN MOD(pb.pb177, 8) > 3 THEN COALESCE(mm.b706,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb177, 4) > 1 THEN COALESCE(mm.b707,  0) ELSE 0 END + CASE WHEN MOD(pb.pb177, 2) = 1 THEN COALESCE(mm.b708,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb178, 16)> 7 THEN COALESCE(mm.b709,  0) ELSE 0 END + CASE WHEN MOD(pb.pb178, 8) > 3 THEN COALESCE(mm.b710,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb178, 4) > 1 THEN COALESCE(mm.b711,  0) ELSE 0 END + CASE WHEN MOD(pb.pb178, 2) = 1 THEN COALESCE(mm.b712,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb179, 16)> 7 THEN COALESCE(mm.b713,  0) ELSE 0 END + CASE WHEN MOD(pb.pb179, 8) > 3 THEN COALESCE(mm.b714,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb179, 4) > 1 THEN COALESCE(mm.b715,  0) ELSE 0 END + CASE WHEN MOD(pb.pb179, 2) = 1 THEN COALESCE(mm.b716,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb180, 16)> 7 THEN COALESCE(mm.b717,  0) ELSE 0 END + CASE WHEN MOD(pb.pb180, 8) > 3 THEN COALESCE(mm.b718,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb180, 4) > 1 THEN COALESCE(mm.b719,  0) ELSE 0 END + CASE WHEN MOD(pb.pb180, 2) = 1 THEN COALESCE(mm.b720,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb181, 16)> 7 THEN COALESCE(mm.b721,  0) ELSE 0 END + CASE WHEN MOD(pb.pb181, 8) > 3 THEN COALESCE(mm.b722,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb181, 4) > 1 THEN COALESCE(mm.b723,  0) ELSE 0 END + CASE WHEN MOD(pb.pb181, 2) = 1 THEN COALESCE(mm.b724,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb182, 16)> 7 THEN COALESCE(mm.b725,  0) ELSE 0 END + CASE WHEN MOD(pb.pb182, 8) > 3 THEN COALESCE(mm.b726,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb182, 4) > 1 THEN COALESCE(mm.b727,  0) ELSE 0 END + CASE WHEN MOD(pb.pb182, 2) = 1 THEN COALESCE(mm.b728,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb183, 16)> 7 THEN COALESCE(mm.b729,  0) ELSE 0 END + CASE WHEN MOD(pb.pb183, 8) > 3 THEN COALESCE(mm.b730,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb183, 4) > 1 THEN COALESCE(mm.b731,  0) ELSE 0 END + CASE WHEN MOD(pb.pb183, 2) = 1 THEN COALESCE(mm.b732,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb184, 16)> 7 THEN COALESCE(mm.b733,  0) ELSE 0 END + CASE WHEN MOD(pb.pb184, 8) > 3 THEN COALESCE(mm.b734,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb184, 4) > 1 THEN COALESCE(mm.b735,  0) ELSE 0 END + CASE WHEN MOD(pb.pb184, 2) = 1 THEN COALESCE(mm.b736,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb185, 16)> 7 THEN COALESCE(mm.b737,  0) ELSE 0 END + CASE WHEN MOD(pb.pb185, 8) > 3 THEN COALESCE(mm.b738,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb185, 4) > 1 THEN COALESCE(mm.b739,  0) ELSE 0 END + CASE WHEN MOD(pb.pb185, 2) = 1 THEN COALESCE(mm.b740,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb186, 16)> 7 THEN COALESCE(mm.b741,  0) ELSE 0 END + CASE WHEN MOD(pb.pb186, 8) > 3 THEN COALESCE(mm.b742,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb186, 4) > 1 THEN COALESCE(mm.b743,  0) ELSE 0 END + CASE WHEN MOD(pb.pb186, 2) = 1 THEN COALESCE(mm.b744,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb187, 16)> 7 THEN COALESCE(mm.b745,  0) ELSE 0 END + CASE WHEN MOD(pb.pb187, 8) > 3 THEN COALESCE(mm.b746,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb187, 4) > 1 THEN COALESCE(mm.b747,  0) ELSE 0 END + CASE WHEN MOD(pb.pb187, 2) = 1 THEN COALESCE(mm.b748,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb188, 16)> 7 THEN COALESCE(mm.b749,  0) ELSE 0 END + CASE WHEN MOD(pb.pb188, 8) > 3 THEN COALESCE(mm.b750,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb188, 4) > 1 THEN COALESCE(mm.b751,  0) ELSE 0 END + CASE WHEN MOD(pb.pb188, 2) = 1 THEN COALESCE(mm.b752,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb189, 16)> 7 THEN COALESCE(mm.b753,  0) ELSE 0 END + CASE WHEN MOD(pb.pb189, 8) > 3 THEN COALESCE(mm.b754,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb189, 4) > 1 THEN COALESCE(mm.b755,  0) ELSE 0 END + CASE WHEN MOD(pb.pb189, 2) = 1 THEN COALESCE(mm.b756,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb190, 16)> 7 THEN COALESCE(mm.b757,  0) ELSE 0 END + CASE WHEN MOD(pb.pb190, 8) > 3 THEN COALESCE(mm.b758,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb190, 4) > 1 THEN COALESCE(mm.b759,  0) ELSE 0 END + CASE WHEN MOD(pb.pb190, 2) = 1 THEN COALESCE(mm.b760,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb191, 16)> 7 THEN COALESCE(mm.b761,  0) ELSE 0 END + CASE WHEN MOD(pb.pb191, 8) > 3 THEN COALESCE(mm.b762,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb191, 4) > 1 THEN COALESCE(mm.b763,  0) ELSE 0 END + CASE WHEN MOD(pb.pb191, 2) = 1 THEN COALESCE(mm.b764,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb192, 16)> 7 THEN COALESCE(mm.b765,  0) ELSE 0 END + CASE WHEN MOD(pb.pb192, 8) > 3 THEN COALESCE(mm.b766,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb192, 4) > 1 THEN COALESCE(mm.b767,  0) ELSE 0 END + CASE WHEN MOD(pb.pb192, 2) = 1 THEN COALESCE(mm.b768,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb193, 16)> 7 THEN COALESCE(mm.b769,  0) ELSE 0 END + CASE WHEN MOD(pb.pb193, 8) > 3 THEN COALESCE(mm.b770,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb193, 4) > 1 THEN COALESCE(mm.b771,  0) ELSE 0 END + CASE WHEN MOD(pb.pb193, 2) = 1 THEN COALESCE(mm.b772,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb194, 16)> 7 THEN COALESCE(mm.b773,  0) ELSE 0 END + CASE WHEN MOD(pb.pb194, 8) > 3 THEN COALESCE(mm.b774,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb194, 4) > 1 THEN COALESCE(mm.b775,  0) ELSE 0 END + CASE WHEN MOD(pb.pb194, 2) = 1 THEN COALESCE(mm.b776,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb195, 16)> 7 THEN COALESCE(mm.b777,  0) ELSE 0 END + CASE WHEN MOD(pb.pb195, 8) > 3 THEN COALESCE(mm.b778,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb195, 4) > 1 THEN COALESCE(mm.b779,  0) ELSE 0 END + CASE WHEN MOD(pb.pb195, 2) = 1 THEN COALESCE(mm.b780,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb196, 16)> 7 THEN COALESCE(mm.b781,  0) ELSE 0 END + CASE WHEN MOD(pb.pb196, 8) > 3 THEN COALESCE(mm.b782,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb196, 4) > 1 THEN COALESCE(mm.b783,  0) ELSE 0 END + CASE WHEN MOD(pb.pb196, 2) = 1 THEN COALESCE(mm.b784,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb197, 16)> 7 THEN COALESCE(mm.b785,  0) ELSE 0 END + CASE WHEN MOD(pb.pb197, 8) > 3 THEN COALESCE(mm.b786,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb197, 4) > 1 THEN COALESCE(mm.b787,  0) ELSE 0 END + CASE WHEN MOD(pb.pb197, 2) = 1 THEN COALESCE(mm.b788,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb198, 16)> 7 THEN COALESCE(mm.b789,  0) ELSE 0 END + CASE WHEN MOD(pb.pb198, 8) > 3 THEN COALESCE(mm.b790,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb198, 4) > 1 THEN COALESCE(mm.b791,  0) ELSE 0 END + CASE WHEN MOD(pb.pb198, 2) = 1 THEN COALESCE(mm.b792,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb199, 16)> 7 THEN COALESCE(mm.b793,  0) ELSE 0 END + CASE WHEN MOD(pb.pb199, 8) > 3 THEN COALESCE(mm.b794,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb199, 4) > 1 THEN COALESCE(mm.b795,  0) ELSE 0 END + CASE WHEN MOD(pb.pb199, 2) = 1 THEN COALESCE(mm.b796,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb200, 16)> 7 THEN COALESCE(mm.b797,  0) ELSE 0 END + CASE WHEN MOD(pb.pb200, 8) > 3 THEN COALESCE(mm.b798,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb200, 4) > 1 THEN COALESCE(mm.b799,  0) ELSE 0 END + CASE WHEN MOD(pb.pb200, 2) = 1 THEN COALESCE(mm.b800,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb201, 16)> 7 THEN COALESCE(mm.b801,  0) ELSE 0 END + CASE WHEN MOD(pb.pb201, 8) > 3 THEN COALESCE(mm.b802,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb201, 4) > 1 THEN COALESCE(mm.b803,  0) ELSE 0 END + CASE WHEN MOD(pb.pb201, 2) = 1 THEN COALESCE(mm.b804,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb202, 16)> 7 THEN COALESCE(mm.b805,  0) ELSE 0 END + CASE WHEN MOD(pb.pb202, 8) > 3 THEN COALESCE(mm.b806,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb202, 4) > 1 THEN COALESCE(mm.b807,  0) ELSE 0 END + CASE WHEN MOD(pb.pb202, 2) = 1 THEN COALESCE(mm.b808,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb203, 16)> 7 THEN COALESCE(mm.b809,  0) ELSE 0 END + CASE WHEN MOD(pb.pb203, 8) > 3 THEN COALESCE(mm.b810,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb203, 4) > 1 THEN COALESCE(mm.b811,  0) ELSE 0 END + CASE WHEN MOD(pb.pb203, 2) = 1 THEN COALESCE(mm.b812,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb204, 16)> 7 THEN COALESCE(mm.b813,  0) ELSE 0 END + CASE WHEN MOD(pb.pb204, 8) > 3 THEN COALESCE(mm.b814,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb204, 4) > 1 THEN COALESCE(mm.b815,  0) ELSE 0 END + CASE WHEN MOD(pb.pb204, 2) = 1 THEN COALESCE(mm.b816,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb205, 16)> 7 THEN COALESCE(mm.b817,  0) ELSE 0 END + CASE WHEN MOD(pb.pb205, 8) > 3 THEN COALESCE(mm.b818,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb205, 4) > 1 THEN COALESCE(mm.b819,  0) ELSE 0 END + CASE WHEN MOD(pb.pb205, 2) = 1 THEN COALESCE(mm.b820,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb206, 16)> 7 THEN COALESCE(mm.b821,  0) ELSE 0 END + CASE WHEN MOD(pb.pb206, 8) > 3 THEN COALESCE(mm.b822,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb206, 4) > 1 THEN COALESCE(mm.b823,  0) ELSE 0 END + CASE WHEN MOD(pb.pb206, 2) = 1 THEN COALESCE(mm.b824,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb207, 16)> 7 THEN COALESCE(mm.b825,  0) ELSE 0 END + CASE WHEN MOD(pb.pb207, 8) > 3 THEN COALESCE(mm.b826,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb207, 4) > 1 THEN COALESCE(mm.b827,  0) ELSE 0 END + CASE WHEN MOD(pb.pb207, 2) = 1 THEN COALESCE(mm.b828,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb208, 16)> 7 THEN COALESCE(mm.b829,  0) ELSE 0 END + CASE WHEN MOD(pb.pb208, 8) > 3 THEN COALESCE(mm.b830,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb208, 4) > 1 THEN COALESCE(mm.b831,  0) ELSE 0 END + CASE WHEN MOD(pb.pb208, 2) = 1 THEN COALESCE(mm.b832,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb209, 16)> 7 THEN COALESCE(mm.b833,  0) ELSE 0 END + CASE WHEN MOD(pb.pb209, 8) > 3 THEN COALESCE(mm.b834,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb209, 4) > 1 THEN COALESCE(mm.b835,  0) ELSE 0 END + CASE WHEN MOD(pb.pb209, 2) = 1 THEN COALESCE(mm.b836,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb210, 16)> 7 THEN COALESCE(mm.b837,  0) ELSE 0 END + CASE WHEN MOD(pb.pb210, 8) > 3 THEN COALESCE(mm.b838,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb210, 4) > 1 THEN COALESCE(mm.b839,  0) ELSE 0 END + CASE WHEN MOD(pb.pb210, 2) = 1 THEN COALESCE(mm.b840,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb211, 16)> 7 THEN COALESCE(mm.b841,  0) ELSE 0 END + CASE WHEN MOD(pb.pb211, 8) > 3 THEN COALESCE(mm.b842,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb211, 4) > 1 THEN COALESCE(mm.b843,  0) ELSE 0 END + CASE WHEN MOD(pb.pb211, 2) = 1 THEN COALESCE(mm.b844,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb212, 16)> 7 THEN COALESCE(mm.b845,  0) ELSE 0 END + CASE WHEN MOD(pb.pb212, 8) > 3 THEN COALESCE(mm.b846,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb212, 4) > 1 THEN COALESCE(mm.b847,  0) ELSE 0 END + CASE WHEN MOD(pb.pb212, 2) = 1 THEN COALESCE(mm.b848,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb213, 16)> 7 THEN COALESCE(mm.b849,  0) ELSE 0 END + CASE WHEN MOD(pb.pb213, 8) > 3 THEN COALESCE(mm.b850,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb213, 4) > 1 THEN COALESCE(mm.b851,  0) ELSE 0 END + CASE WHEN MOD(pb.pb213, 2) = 1 THEN COALESCE(mm.b852,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb214, 16)> 7 THEN COALESCE(mm.b853,  0) ELSE 0 END + CASE WHEN MOD(pb.pb214, 8) > 3 THEN COALESCE(mm.b854,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb214, 4) > 1 THEN COALESCE(mm.b855,  0) ELSE 0 END + CASE WHEN MOD(pb.pb214, 2) = 1 THEN COALESCE(mm.b856,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb215, 16)> 7 THEN COALESCE(mm.b857,  0) ELSE 0 END + CASE WHEN MOD(pb.pb215, 8) > 3 THEN COALESCE(mm.b858,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb215, 4) > 1 THEN COALESCE(mm.b859,  0) ELSE 0 END + CASE WHEN MOD(pb.pb215, 2) = 1 THEN COALESCE(mm.b860,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb216, 16)> 7 THEN COALESCE(mm.b861,  0) ELSE 0 END + CASE WHEN MOD(pb.pb216, 8) > 3 THEN COALESCE(mm.b862,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb216, 4) > 1 THEN COALESCE(mm.b863,  0) ELSE 0 END + CASE WHEN MOD(pb.pb216, 2) = 1 THEN COALESCE(mm.b864,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb217, 16)> 7 THEN COALESCE(mm.b865,  0) ELSE 0 END + CASE WHEN MOD(pb.pb217, 8) > 3 THEN COALESCE(mm.b866,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb217, 4) > 1 THEN COALESCE(mm.b867,  0) ELSE 0 END + CASE WHEN MOD(pb.pb217, 2) = 1 THEN COALESCE(mm.b868,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb218, 16)> 7 THEN COALESCE(mm.b869,  0) ELSE 0 END + CASE WHEN MOD(pb.pb218, 8) > 3 THEN COALESCE(mm.b870,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb218, 4) > 1 THEN COALESCE(mm.b871,  0) ELSE 0 END + CASE WHEN MOD(pb.pb218, 2) = 1 THEN COALESCE(mm.b872,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb219, 16)> 7 THEN COALESCE(mm.b873,  0) ELSE 0 END + CASE WHEN MOD(pb.pb219, 8) > 3 THEN COALESCE(mm.b874,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb219, 4) > 1 THEN COALESCE(mm.b875,  0) ELSE 0 END + CASE WHEN MOD(pb.pb219, 2) = 1 THEN COALESCE(mm.b876,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb220, 16)> 7 THEN COALESCE(mm.b877,  0) ELSE 0 END + CASE WHEN MOD(pb.pb220, 8) > 3 THEN COALESCE(mm.b878,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb220, 4) > 1 THEN COALESCE(mm.b879,  0) ELSE 0 END + CASE WHEN MOD(pb.pb220, 2) = 1 THEN COALESCE(mm.b880,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb221, 16)> 7 THEN COALESCE(mm.b881,  0) ELSE 0 END + CASE WHEN MOD(pb.pb221, 8) > 3 THEN COALESCE(mm.b882,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb221, 4) > 1 THEN COALESCE(mm.b883,  0) ELSE 0 END + CASE WHEN MOD(pb.pb221, 2) = 1 THEN COALESCE(mm.b884,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb222, 16)> 7 THEN COALESCE(mm.b885,  0) ELSE 0 END + CASE WHEN MOD(pb.pb222, 8) > 3 THEN COALESCE(mm.b886,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb222, 4) > 1 THEN COALESCE(mm.b887,  0) ELSE 0 END + CASE WHEN MOD(pb.pb222, 2) = 1 THEN COALESCE(mm.b888,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb223, 16)> 7 THEN COALESCE(mm.b889,  0) ELSE 0 END + CASE WHEN MOD(pb.pb223, 8) > 3 THEN COALESCE(mm.b890,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb223, 4) > 1 THEN COALESCE(mm.b891,  0) ELSE 0 END + CASE WHEN MOD(pb.pb223, 2) = 1 THEN COALESCE(mm.b892,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb224, 16)> 7 THEN COALESCE(mm.b893,  0) ELSE 0 END + CASE WHEN MOD(pb.pb224, 8) > 3 THEN COALESCE(mm.b894,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb224, 4) > 1 THEN COALESCE(mm.b895,  0) ELSE 0 END + CASE WHEN MOD(pb.pb224, 2) = 1 THEN COALESCE(mm.b896,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb225, 16)> 7 THEN COALESCE(mm.b897,  0) ELSE 0 END + CASE WHEN MOD(pb.pb225, 8) > 3 THEN COALESCE(mm.b898,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb225, 4) > 1 THEN COALESCE(mm.b899,  0) ELSE 0 END + CASE WHEN MOD(pb.pb225, 2) = 1 THEN COALESCE(mm.b900,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb226, 16)> 7 THEN COALESCE(mm.b901,  0) ELSE 0 END + CASE WHEN MOD(pb.pb226, 8) > 3 THEN COALESCE(mm.b902,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb226, 4) > 1 THEN COALESCE(mm.b903,  0) ELSE 0 END + CASE WHEN MOD(pb.pb226, 2) = 1 THEN COALESCE(mm.b904,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb227, 16)> 7 THEN COALESCE(mm.b905,  0) ELSE 0 END + CASE WHEN MOD(pb.pb227, 8) > 3 THEN COALESCE(mm.b906,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb227, 4) > 1 THEN COALESCE(mm.b907,  0) ELSE 0 END + CASE WHEN MOD(pb.pb227, 2) = 1 THEN COALESCE(mm.b908,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb228, 16)> 7 THEN COALESCE(mm.b909,  0) ELSE 0 END + CASE WHEN MOD(pb.pb228, 8) > 3 THEN COALESCE(mm.b910,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb228, 4) > 1 THEN COALESCE(mm.b911,  0) ELSE 0 END + CASE WHEN MOD(pb.pb228, 2) = 1 THEN COALESCE(mm.b912,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb229, 16)> 7 THEN COALESCE(mm.b913,  0) ELSE 0 END + CASE WHEN MOD(pb.pb229, 8) > 3 THEN COALESCE(mm.b914,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb229, 4) > 1 THEN COALESCE(mm.b915,  0) ELSE 0 END + CASE WHEN MOD(pb.pb229, 2) = 1 THEN COALESCE(mm.b916,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb230, 16)> 7 THEN COALESCE(mm.b917,  0) ELSE 0 END + CASE WHEN MOD(pb.pb230, 8) > 3 THEN COALESCE(mm.b918,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb230, 4) > 1 THEN COALESCE(mm.b919,  0) ELSE 0 END + CASE WHEN MOD(pb.pb230, 2) = 1 THEN COALESCE(mm.b920,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb231, 16)> 7 THEN COALESCE(mm.b921,  0) ELSE 0 END + CASE WHEN MOD(pb.pb231, 8) > 3 THEN COALESCE(mm.b922,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb231, 4) > 1 THEN COALESCE(mm.b923,  0) ELSE 0 END + CASE WHEN MOD(pb.pb231, 2) = 1 THEN COALESCE(mm.b924,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb232, 16)> 7 THEN COALESCE(mm.b925,  0) ELSE 0 END + CASE WHEN MOD(pb.pb232, 8) > 3 THEN COALESCE(mm.b926,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb232, 4) > 1 THEN COALESCE(mm.b927,  0) ELSE 0 END + CASE WHEN MOD(pb.pb232, 2) = 1 THEN COALESCE(mm.b928,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb233, 16)> 7 THEN COALESCE(mm.b929,  0) ELSE 0 END + CASE WHEN MOD(pb.pb233, 8) > 3 THEN COALESCE(mm.b930,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb233, 4) > 1 THEN COALESCE(mm.b931,  0) ELSE 0 END + CASE WHEN MOD(pb.pb233, 2) = 1 THEN COALESCE(mm.b932,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb234, 16)> 7 THEN COALESCE(mm.b933,  0) ELSE 0 END + CASE WHEN MOD(pb.pb234, 8) > 3 THEN COALESCE(mm.b934,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb234, 4) > 1 THEN COALESCE(mm.b935,  0) ELSE 0 END + CASE WHEN MOD(pb.pb234, 2) = 1 THEN COALESCE(mm.b936,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb235, 16)> 7 THEN COALESCE(mm.b937,  0) ELSE 0 END + CASE WHEN MOD(pb.pb235, 8) > 3 THEN COALESCE(mm.b938,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb235, 4) > 1 THEN COALESCE(mm.b939,  0) ELSE 0 END + CASE WHEN MOD(pb.pb235, 2) = 1 THEN COALESCE(mm.b940,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb236, 16)> 7 THEN COALESCE(mm.b941,  0) ELSE 0 END + CASE WHEN MOD(pb.pb236, 8) > 3 THEN COALESCE(mm.b942,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb236, 4) > 1 THEN COALESCE(mm.b943,  0) ELSE 0 END + CASE WHEN MOD(pb.pb236, 2) = 1 THEN COALESCE(mm.b944,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb237, 16)> 7 THEN COALESCE(mm.b945,  0) ELSE 0 END + CASE WHEN MOD(pb.pb237, 8) > 3 THEN COALESCE(mm.b946,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb237, 4) > 1 THEN COALESCE(mm.b947,  0) ELSE 0 END + CASE WHEN MOD(pb.pb237, 2) = 1 THEN COALESCE(mm.b948,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb238, 16)> 7 THEN COALESCE(mm.b949,  0) ELSE 0 END + CASE WHEN MOD(pb.pb238, 8) > 3 THEN COALESCE(mm.b950,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb238, 4) > 1 THEN COALESCE(mm.b951,  0) ELSE 0 END + CASE WHEN MOD(pb.pb238, 2) = 1 THEN COALESCE(mm.b952,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb239, 16)> 7 THEN COALESCE(mm.b953,  0) ELSE 0 END + CASE WHEN MOD(pb.pb239, 8) > 3 THEN COALESCE(mm.b954,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb239, 4) > 1 THEN COALESCE(mm.b955,  0) ELSE 0 END + CASE WHEN MOD(pb.pb239, 2) = 1 THEN COALESCE(mm.b956,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb240, 16)> 7 THEN COALESCE(mm.b957,  0) ELSE 0 END + CASE WHEN MOD(pb.pb240, 8) > 3 THEN COALESCE(mm.b958,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb240, 4) > 1 THEN COALESCE(mm.b959,  0) ELSE 0 END + CASE WHEN MOD(pb.pb240, 2) = 1 THEN COALESCE(mm.b960,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb241, 16)> 7 THEN COALESCE(mm.b961,  0) ELSE 0 END + CASE WHEN MOD(pb.pb241, 8) > 3 THEN COALESCE(mm.b962,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb241, 4) > 1 THEN COALESCE(mm.b963,  0) ELSE 0 END + CASE WHEN MOD(pb.pb241, 2) = 1 THEN COALESCE(mm.b964,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb242, 16)> 7 THEN COALESCE(mm.b965,  0) ELSE 0 END + CASE WHEN MOD(pb.pb242, 8) > 3 THEN COALESCE(mm.b966,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb242, 4) > 1 THEN COALESCE(mm.b967,  0) ELSE 0 END + CASE WHEN MOD(pb.pb242, 2) = 1 THEN COALESCE(mm.b968,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb243, 16)> 7 THEN COALESCE(mm.b969,  0) ELSE 0 END + CASE WHEN MOD(pb.pb243, 8) > 3 THEN COALESCE(mm.b970,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb243, 4) > 1 THEN COALESCE(mm.b971,  0) ELSE 0 END + CASE WHEN MOD(pb.pb243, 2) = 1 THEN COALESCE(mm.b972,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb244, 16)> 7 THEN COALESCE(mm.b973,  0) ELSE 0 END + CASE WHEN MOD(pb.pb244, 8) > 3 THEN COALESCE(mm.b974,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb244, 4) > 1 THEN COALESCE(mm.b975,  0) ELSE 0 END + CASE WHEN MOD(pb.pb244, 2) = 1 THEN COALESCE(mm.b976,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb245, 16)> 7 THEN COALESCE(mm.b977,  0) ELSE 0 END + CASE WHEN MOD(pb.pb245, 8) > 3 THEN COALESCE(mm.b978,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb245, 4) > 1 THEN COALESCE(mm.b979,  0) ELSE 0 END + CASE WHEN MOD(pb.pb245, 2) = 1 THEN COALESCE(mm.b980,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb246, 16)> 7 THEN COALESCE(mm.b981,  0) ELSE 0 END + CASE WHEN MOD(pb.pb246, 8) > 3 THEN COALESCE(mm.b982,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb246, 4) > 1 THEN COALESCE(mm.b983,  0) ELSE 0 END + CASE WHEN MOD(pb.pb246, 2) = 1 THEN COALESCE(mm.b984,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb247, 16)> 7 THEN COALESCE(mm.b985,  0) ELSE 0 END + CASE WHEN MOD(pb.pb247, 8) > 3 THEN COALESCE(mm.b986,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb247, 4) > 1 THEN COALESCE(mm.b987,  0) ELSE 0 END + CASE WHEN MOD(pb.pb247, 2) = 1 THEN COALESCE(mm.b988,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb248, 16)> 7 THEN COALESCE(mm.b989,  0) ELSE 0 END + CASE WHEN MOD(pb.pb248, 8) > 3 THEN COALESCE(mm.b990,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb248, 4) > 1 THEN COALESCE(mm.b991,  0) ELSE 0 END + CASE WHEN MOD(pb.pb248, 2) = 1 THEN COALESCE(mm.b992,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb249, 16)> 7 THEN COALESCE(mm.b993,  0) ELSE 0 END + CASE WHEN MOD(pb.pb249, 8) > 3 THEN COALESCE(mm.b994,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb249, 4) > 1 THEN COALESCE(mm.b995,  0) ELSE 0 END + CASE WHEN MOD(pb.pb249, 2) = 1 THEN COALESCE(mm.b996,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb250, 16)> 7 THEN COALESCE(mm.b997,  0) ELSE 0 END + CASE WHEN MOD(pb.pb250, 8) > 3 THEN COALESCE(mm.b998,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb250, 4) > 1 THEN COALESCE(mm.b999,  0) ELSE 0 END + CASE WHEN MOD(pb.pb250, 2) = 1 THEN COALESCE(mm.b1000,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb251, 16)> 7 THEN COALESCE(mm.b1001,  0) ELSE 0 END + CASE WHEN MOD(pb.pb251, 8) > 3 THEN COALESCE(mm.b1002,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb251, 4) > 1 THEN COALESCE(mm.b1003,  0) ELSE 0 END + CASE WHEN MOD(pb.pb251, 2) = 1 THEN COALESCE(mm.b1004,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb252, 16)> 7 THEN COALESCE(mm.b1005,  0) ELSE 0 END + CASE WHEN MOD(pb.pb252, 8) > 3 THEN COALESCE(mm.b1006,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb252, 4) > 1 THEN COALESCE(mm.b1007,  0) ELSE 0 END + CASE WHEN MOD(pb.pb252, 2) = 1 THEN COALESCE(mm.b1008,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb253, 16)> 7 THEN COALESCE(mm.b1009,  0) ELSE 0 END + CASE WHEN MOD(pb.pb253, 8) > 3 THEN COALESCE(mm.b1010,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb253, 4) > 1 THEN COALESCE(mm.b1011,  0) ELSE 0 END + CASE WHEN MOD(pb.pb253, 2) = 1 THEN COALESCE(mm.b1012,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb254, 16)> 7 THEN COALESCE(mm.b1013,  0) ELSE 0 END + CASE WHEN MOD(pb.pb254, 8) > 3 THEN COALESCE(mm.b1014,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb254, 4) > 1 THEN COALESCE(mm.b1015,  0) ELSE 0 END + CASE WHEN MOD(pb.pb254, 2) = 1 THEN COALESCE(mm.b1016,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb255, 16)> 7 THEN COALESCE(mm.b1017,  0) ELSE 0 END + CASE WHEN MOD(pb.pb255, 8) > 3 THEN COALESCE(mm.b1018,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb255, 4) > 1 THEN COALESCE(mm.b1019,  0) ELSE 0 END + CASE WHEN MOD(pb.pb255, 2) = 1 THEN COALESCE(mm.b1020,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb256, 16)> 7 THEN COALESCE(mm.b1021,  0) ELSE 0 END + CASE WHEN MOD(pb.pb256, 8) > 3 THEN COALESCE(mm.b1022,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb256, 4) > 1 THEN COALESCE(mm.b1023,  0) ELSE 0 END + CASE WHEN MOD(pb.pb256, 2) = 1 THEN COALESCE(mm.b1024,  0) ELSE 0 END               
          ) as yld_testdie, MAX(pb.pass_bin_set) as PassBinList FROM mm,pb ),
     yld_deduct as (SELECT SUM(
           CASE WHEN MOD(pb.pb01, 16)> 7 THEN COALESCE(dd.D01,  0) ELSE 0 END + CASE WHEN MOD(pb.pb01, 8) > 3 THEN COALESCE(dd.D02,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb01, 4) > 1 THEN COALESCE(dd.D03,  0) ELSE 0 END + CASE WHEN MOD(pb.pb01, 2) = 1 THEN COALESCE(dd.D04,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb02, 16)> 7 THEN COALESCE(dd.D05,  0) ELSE 0 END + CASE WHEN MOD(pb.pb02, 8) > 3 THEN COALESCE(dd.D06,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb02, 4) > 1 THEN COALESCE(dd.D07,  0) ELSE 0 END + CASE WHEN MOD(pb.pb02, 2) = 1 THEN COALESCE(dd.D08,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb03, 16)> 7 THEN COALESCE(dd.D09,  0) ELSE 0 END + CASE WHEN MOD(pb.pb03, 8) > 3 THEN COALESCE(dd.D10,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb03, 4) > 1 THEN COALESCE(dd.D11,  0) ELSE 0 END + CASE WHEN MOD(pb.pb03, 2) = 1 THEN COALESCE(dd.D12,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb04, 16)> 7 THEN COALESCE(dd.D13,  0) ELSE 0 END + CASE WHEN MOD(pb.pb04, 8) > 3 THEN COALESCE(dd.D14,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb04, 4) > 1 THEN COALESCE(dd.D15,  0) ELSE 0 END + CASE WHEN MOD(pb.pb04, 2) = 1 THEN COALESCE(dd.D16,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb05, 16)> 7 THEN COALESCE(dd.D17,  0) ELSE 0 END + CASE WHEN MOD(pb.pb05, 8) > 3 THEN COALESCE(dd.D18,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb05, 4) > 1 THEN COALESCE(dd.D19,  0) ELSE 0 END + CASE WHEN MOD(pb.pb05, 2) = 1 THEN COALESCE(dd.D20,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb06, 16)> 7 THEN COALESCE(dd.D21,  0) ELSE 0 END + CASE WHEN MOD(pb.pb06, 8) > 3 THEN COALESCE(dd.D22,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb06, 4) > 1 THEN COALESCE(dd.D23,  0) ELSE 0 END + CASE WHEN MOD(pb.pb06, 2) = 1 THEN COALESCE(dd.D24,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb07, 16)> 7 THEN COALESCE(dd.D25,  0) ELSE 0 END + CASE WHEN MOD(pb.pb07, 8) > 3 THEN COALESCE(dd.D26,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb07, 4) > 1 THEN COALESCE(dd.D27,  0) ELSE 0 END + CASE WHEN MOD(pb.pb07, 2) = 1 THEN COALESCE(dd.D28,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb08, 16)> 7 THEN COALESCE(dd.D29,  0) ELSE 0 END + CASE WHEN MOD(pb.pb08, 8) > 3 THEN COALESCE(dd.D30,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb08, 4) > 1 THEN COALESCE(dd.D31,  0) ELSE 0 END + CASE WHEN MOD(pb.pb08, 2) = 1 THEN COALESCE(dd.D32,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb09, 16)> 7 THEN COALESCE(dd.D33,  0) ELSE 0 END + CASE WHEN MOD(pb.pb09, 8) > 3 THEN COALESCE(dd.D34,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb09, 4) > 1 THEN COALESCE(dd.D35,  0) ELSE 0 END + CASE WHEN MOD(pb.pb09, 2) = 1 THEN COALESCE(dd.D36,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb10, 16)> 7 THEN COALESCE(dd.D37,  0) ELSE 0 END + CASE WHEN MOD(pb.pb10, 8) > 3 THEN COALESCE(dd.D38,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb10, 4) > 1 THEN COALESCE(dd.D39,  0) ELSE 0 END + CASE WHEN MOD(pb.pb10, 2) = 1 THEN COALESCE(dd.D40,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb11, 16)> 7 THEN COALESCE(dd.D41,  0) ELSE 0 END + CASE WHEN MOD(pb.pb11, 8) > 3 THEN COALESCE(dd.D42,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb11, 4) > 1 THEN COALESCE(dd.D43,  0) ELSE 0 END + CASE WHEN MOD(pb.pb11, 2) = 1 THEN COALESCE(dd.D44,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb12, 16)> 7 THEN COALESCE(dd.D45,  0) ELSE 0 END + CASE WHEN MOD(pb.pb12, 8) > 3 THEN COALESCE(dd.D46,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb12, 4) > 1 THEN COALESCE(dd.D47,  0) ELSE 0 END + CASE WHEN MOD(pb.pb12, 2) = 1 THEN COALESCE(dd.D48,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb13, 16)> 7 THEN COALESCE(dd.D49,  0) ELSE 0 END + CASE WHEN MOD(pb.pb13, 8) > 3 THEN COALESCE(dd.D50,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb13, 4) > 1 THEN COALESCE(dd.D51,  0) ELSE 0 END + CASE WHEN MOD(pb.pb13, 2) = 1 THEN COALESCE(dd.D52,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb14, 16)> 7 THEN COALESCE(dd.D53,  0) ELSE 0 END + CASE WHEN MOD(pb.pb14, 8) > 3 THEN COALESCE(dd.D54,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb14, 4) > 1 THEN COALESCE(dd.D55,  0) ELSE 0 END + CASE WHEN MOD(pb.pb14, 2) = 1 THEN COALESCE(dd.D56,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb15, 16)> 7 THEN COALESCE(dd.D57,  0) ELSE 0 END + CASE WHEN MOD(pb.pb15, 8) > 3 THEN COALESCE(dd.D58,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb15, 4) > 1 THEN COALESCE(dd.D59,  0) ELSE 0 END + CASE WHEN MOD(pb.pb15, 2) = 1 THEN COALESCE(dd.D60,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb16, 16)> 7 THEN COALESCE(dd.D61,  0) ELSE 0 END + CASE WHEN MOD(pb.pb16, 8) > 3 THEN COALESCE(dd.D62,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb16, 4) > 1 THEN COALESCE(dd.D63,  0) ELSE 0 END + CASE WHEN MOD(pb.pb16, 2) = 1 THEN COALESCE(dd.D64,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb17, 16)> 7 THEN COALESCE(dd.D65,  0) ELSE 0 END + CASE WHEN MOD(pb.pb17, 8) > 3 THEN COALESCE(dd.D66,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb17, 4) > 1 THEN COALESCE(dd.D67,  0) ELSE 0 END + CASE WHEN MOD(pb.pb17, 2) = 1 THEN COALESCE(dd.D68,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb18, 16)> 7 THEN COALESCE(dd.D69,  0) ELSE 0 END + CASE WHEN MOD(pb.pb18, 8) > 3 THEN COALESCE(dd.D70,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb18, 4) > 1 THEN COALESCE(dd.D71,  0) ELSE 0 END + CASE WHEN MOD(pb.pb18, 2) = 1 THEN COALESCE(dd.D72,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb19, 16)> 7 THEN COALESCE(dd.D73,  0) ELSE 0 END + CASE WHEN MOD(pb.pb19, 8) > 3 THEN COALESCE(dd.D74,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb19, 4) > 1 THEN COALESCE(dd.D75,  0) ELSE 0 END + CASE WHEN MOD(pb.pb19, 2) = 1 THEN COALESCE(dd.D76,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb20, 16)> 7 THEN COALESCE(dd.D77,  0) ELSE 0 END + CASE WHEN MOD(pb.pb20, 8) > 3 THEN COALESCE(dd.D78,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb20, 4) > 1 THEN COALESCE(dd.D79,  0) ELSE 0 END + CASE WHEN MOD(pb.pb20, 2) = 1 THEN COALESCE(dd.D80,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb21, 16)> 7 THEN COALESCE(dd.D81,  0) ELSE 0 END + CASE WHEN MOD(pb.pb21, 8) > 3 THEN COALESCE(dd.D82,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb21, 4) > 1 THEN COALESCE(dd.D83,  0) ELSE 0 END + CASE WHEN MOD(pb.pb21, 2) = 1 THEN COALESCE(dd.D84,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb22, 16)> 7 THEN COALESCE(dd.D85,  0) ELSE 0 END + CASE WHEN MOD(pb.pb22, 8) > 3 THEN COALESCE(dd.D86,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb22, 4) > 1 THEN COALESCE(dd.D87,  0) ELSE 0 END + CASE WHEN MOD(pb.pb22, 2) = 1 THEN COALESCE(dd.D88,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb23, 16)> 7 THEN COALESCE(dd.D89,  0) ELSE 0 END + CASE WHEN MOD(pb.pb23, 8) > 3 THEN COALESCE(dd.D90,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb23, 4) > 1 THEN COALESCE(dd.D91,  0) ELSE 0 END + CASE WHEN MOD(pb.pb23, 2) = 1 THEN COALESCE(dd.D92,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb24, 16)> 7 THEN COALESCE(dd.D93,  0) ELSE 0 END + CASE WHEN MOD(pb.pb24, 8) > 3 THEN COALESCE(dd.D94,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb24, 4) > 1 THEN COALESCE(dd.D95,  0) ELSE 0 END + CASE WHEN MOD(pb.pb24, 2) = 1 THEN COALESCE(dd.D96,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb25, 16)> 7 THEN COALESCE(dd.D97,  0) ELSE 0 END + CASE WHEN MOD(pb.pb25, 8) > 3 THEN COALESCE(dd.D98,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb25, 4) > 1 THEN COALESCE(dd.D99,  0) ELSE 0 END + CASE WHEN MOD(pb.pb25, 2) = 1 THEN COALESCE(dd.D100,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb26, 16)> 7 THEN COALESCE(dd.D101,  0) ELSE 0 END + CASE WHEN MOD(pb.pb26, 8) > 3 THEN COALESCE(dd.D102,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb26, 4) > 1 THEN COALESCE(dd.D103,  0) ELSE 0 END + CASE WHEN MOD(pb.pb26, 2) = 1 THEN COALESCE(dd.D104,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb27, 16)> 7 THEN COALESCE(dd.D105,  0) ELSE 0 END + CASE WHEN MOD(pb.pb27, 8) > 3 THEN COALESCE(dd.D106,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb27, 4) > 1 THEN COALESCE(dd.D107,  0) ELSE 0 END + CASE WHEN MOD(pb.pb27, 2) = 1 THEN COALESCE(dd.D108,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb28, 16)> 7 THEN COALESCE(dd.D109,  0) ELSE 0 END + CASE WHEN MOD(pb.pb28, 8) > 3 THEN COALESCE(dd.D110,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb28, 4) > 1 THEN COALESCE(dd.D111,  0) ELSE 0 END + CASE WHEN MOD(pb.pb28, 2) = 1 THEN COALESCE(dd.D112,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb29, 16)> 7 THEN COALESCE(dd.D113,  0) ELSE 0 END + CASE WHEN MOD(pb.pb29, 8) > 3 THEN COALESCE(dd.D114,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb29, 4) > 1 THEN COALESCE(dd.D115,  0) ELSE 0 END + CASE WHEN MOD(pb.pb29, 2) = 1 THEN COALESCE(dd.D116,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb30, 16)> 7 THEN COALESCE(dd.D117,  0) ELSE 0 END + CASE WHEN MOD(pb.pb30, 8) > 3 THEN COALESCE(dd.D118,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb30, 4) > 1 THEN COALESCE(dd.D119,  0) ELSE 0 END + CASE WHEN MOD(pb.pb30, 2) = 1 THEN COALESCE(dd.D120,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb31, 16)> 7 THEN COALESCE(dd.D121,  0) ELSE 0 END + CASE WHEN MOD(pb.pb31, 8) > 3 THEN COALESCE(dd.D122,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb31, 4) > 1 THEN COALESCE(dd.D123,  0) ELSE 0 END + CASE WHEN MOD(pb.pb31, 2) = 1 THEN COALESCE(dd.D124,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb32, 16)> 7 THEN COALESCE(dd.D125,  0) ELSE 0 END + CASE WHEN MOD(pb.pb32, 8) > 3 THEN COALESCE(dd.D126,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb32, 4) > 1 THEN COALESCE(dd.D127,  0) ELSE 0 END + CASE WHEN MOD(pb.pb32, 2) = 1 THEN COALESCE(dd.D128,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb33, 16)> 7 THEN COALESCE(dd.D129,  0) ELSE 0 END + CASE WHEN MOD(pb.pb33, 8) > 3 THEN COALESCE(dd.D130,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb33, 4) > 1 THEN COALESCE(dd.D131,  0) ELSE 0 END + CASE WHEN MOD(pb.pb33, 2) = 1 THEN COALESCE(dd.D132,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb34, 16)> 7 THEN COALESCE(dd.D133,  0) ELSE 0 END + CASE WHEN MOD(pb.pb34, 8) > 3 THEN COALESCE(dd.D134,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb34, 4) > 1 THEN COALESCE(dd.D135,  0) ELSE 0 END + CASE WHEN MOD(pb.pb34, 2) = 1 THEN COALESCE(dd.D136,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb35, 16)> 7 THEN COALESCE(dd.D137,  0) ELSE 0 END + CASE WHEN MOD(pb.pb35, 8) > 3 THEN COALESCE(dd.D138,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb35, 4) > 1 THEN COALESCE(dd.D139,  0) ELSE 0 END + CASE WHEN MOD(pb.pb35, 2) = 1 THEN COALESCE(dd.D140,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb36, 16)> 7 THEN COALESCE(dd.D141,  0) ELSE 0 END + CASE WHEN MOD(pb.pb36, 8) > 3 THEN COALESCE(dd.D142,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb36, 4) > 1 THEN COALESCE(dd.D143,  0) ELSE 0 END + CASE WHEN MOD(pb.pb36, 2) = 1 THEN COALESCE(dd.D144,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb37, 16)> 7 THEN COALESCE(dd.D145,  0) ELSE 0 END + CASE WHEN MOD(pb.pb37, 8) > 3 THEN COALESCE(dd.D146,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb37, 4) > 1 THEN COALESCE(dd.D147,  0) ELSE 0 END + CASE WHEN MOD(pb.pb37, 2) = 1 THEN COALESCE(dd.D148,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb38, 16)> 7 THEN COALESCE(dd.D149,  0) ELSE 0 END + CASE WHEN MOD(pb.pb38, 8) > 3 THEN COALESCE(dd.D150,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb38, 4) > 1 THEN COALESCE(dd.D151,  0) ELSE 0 END + CASE WHEN MOD(pb.pb38, 2) = 1 THEN COALESCE(dd.D152,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb39, 16)> 7 THEN COALESCE(dd.D153,  0) ELSE 0 END + CASE WHEN MOD(pb.pb39, 8) > 3 THEN COALESCE(dd.D154,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb39, 4) > 1 THEN COALESCE(dd.D155,  0) ELSE 0 END + CASE WHEN MOD(pb.pb39, 2) = 1 THEN COALESCE(dd.D156,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb40, 16)> 7 THEN COALESCE(dd.D157,  0) ELSE 0 END + CASE WHEN MOD(pb.pb40, 8) > 3 THEN COALESCE(dd.D158,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb40, 4) > 1 THEN COALESCE(dd.D159,  0) ELSE 0 END + CASE WHEN MOD(pb.pb40, 2) = 1 THEN COALESCE(dd.D160,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb41, 16)> 7 THEN COALESCE(dd.D161,  0) ELSE 0 END + CASE WHEN MOD(pb.pb41, 8) > 3 THEN COALESCE(dd.D162,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb41, 4) > 1 THEN COALESCE(dd.D163,  0) ELSE 0 END + CASE WHEN MOD(pb.pb41, 2) = 1 THEN COALESCE(dd.D164,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb42, 16)> 7 THEN COALESCE(dd.D165,  0) ELSE 0 END + CASE WHEN MOD(pb.pb42, 8) > 3 THEN COALESCE(dd.D166,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb42, 4) > 1 THEN COALESCE(dd.D167,  0) ELSE 0 END + CASE WHEN MOD(pb.pb42, 2) = 1 THEN COALESCE(dd.D168,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb43, 16)> 7 THEN COALESCE(dd.D169,  0) ELSE 0 END + CASE WHEN MOD(pb.pb43, 8) > 3 THEN COALESCE(dd.D170,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb43, 4) > 1 THEN COALESCE(dd.D171,  0) ELSE 0 END + CASE WHEN MOD(pb.pb43, 2) = 1 THEN COALESCE(dd.D172,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb44, 16)> 7 THEN COALESCE(dd.D173,  0) ELSE 0 END + CASE WHEN MOD(pb.pb44, 8) > 3 THEN COALESCE(dd.D174,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb44, 4) > 1 THEN COALESCE(dd.D175,  0) ELSE 0 END + CASE WHEN MOD(pb.pb44, 2) = 1 THEN COALESCE(dd.D176,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb45, 16)> 7 THEN COALESCE(dd.D177,  0) ELSE 0 END + CASE WHEN MOD(pb.pb45, 8) > 3 THEN COALESCE(dd.D178,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb45, 4) > 1 THEN COALESCE(dd.D179,  0) ELSE 0 END + CASE WHEN MOD(pb.pb45, 2) = 1 THEN COALESCE(dd.D180,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb46, 16)> 7 THEN COALESCE(dd.D181,  0) ELSE 0 END + CASE WHEN MOD(pb.pb46, 8) > 3 THEN COALESCE(dd.D182,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb46, 4) > 1 THEN COALESCE(dd.D183,  0) ELSE 0 END + CASE WHEN MOD(pb.pb46, 2) = 1 THEN COALESCE(dd.D184,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb47, 16)> 7 THEN COALESCE(dd.D185,  0) ELSE 0 END + CASE WHEN MOD(pb.pb47, 8) > 3 THEN COALESCE(dd.D186,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb47, 4) > 1 THEN COALESCE(dd.D187,  0) ELSE 0 END + CASE WHEN MOD(pb.pb47, 2) = 1 THEN COALESCE(dd.D188,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb48, 16)> 7 THEN COALESCE(dd.D189,  0) ELSE 0 END + CASE WHEN MOD(pb.pb48, 8) > 3 THEN COALESCE(dd.D190,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb48, 4) > 1 THEN COALESCE(dd.D191,  0) ELSE 0 END + CASE WHEN MOD(pb.pb48, 2) = 1 THEN COALESCE(dd.D192,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb49, 16)> 7 THEN COALESCE(dd.D193,  0) ELSE 0 END + CASE WHEN MOD(pb.pb49, 8) > 3 THEN COALESCE(dd.D194,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb49, 4) > 1 THEN COALESCE(dd.D195,  0) ELSE 0 END + CASE WHEN MOD(pb.pb49, 2) = 1 THEN COALESCE(dd.D196,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb50, 16)> 7 THEN COALESCE(dd.D197,  0) ELSE 0 END + CASE WHEN MOD(pb.pb50, 8) > 3 THEN COALESCE(dd.D198,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb50, 4) > 1 THEN COALESCE(dd.D199,  0) ELSE 0 END + CASE WHEN MOD(pb.pb50, 2) = 1 THEN COALESCE(dd.D200,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb51, 16)> 7 THEN COALESCE(dd.D201,  0) ELSE 0 END + CASE WHEN MOD(pb.pb51, 8) > 3 THEN COALESCE(dd.D202,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb51, 4) > 1 THEN COALESCE(dd.D203,  0) ELSE 0 END + CASE WHEN MOD(pb.pb51, 2) = 1 THEN COALESCE(dd.D204,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb52, 16)> 7 THEN COALESCE(dd.D205,  0) ELSE 0 END + CASE WHEN MOD(pb.pb52, 8) > 3 THEN COALESCE(dd.D206,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb52, 4) > 1 THEN COALESCE(dd.D207,  0) ELSE 0 END + CASE WHEN MOD(pb.pb52, 2) = 1 THEN COALESCE(dd.D208,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb53, 16)> 7 THEN COALESCE(dd.D209,  0) ELSE 0 END + CASE WHEN MOD(pb.pb53, 8) > 3 THEN COALESCE(dd.D210,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb53, 4) > 1 THEN COALESCE(dd.D211,  0) ELSE 0 END + CASE WHEN MOD(pb.pb53, 2) = 1 THEN COALESCE(dd.D212,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb54, 16)> 7 THEN COALESCE(dd.D213,  0) ELSE 0 END + CASE WHEN MOD(pb.pb54, 8) > 3 THEN COALESCE(dd.D214,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb54, 4) > 1 THEN COALESCE(dd.D215,  0) ELSE 0 END + CASE WHEN MOD(pb.pb54, 2) = 1 THEN COALESCE(dd.D216,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb55, 16)> 7 THEN COALESCE(dd.D217,  0) ELSE 0 END + CASE WHEN MOD(pb.pb55, 8) > 3 THEN COALESCE(dd.D218,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb55, 4) > 1 THEN COALESCE(dd.D219,  0) ELSE 0 END + CASE WHEN MOD(pb.pb55, 2) = 1 THEN COALESCE(dd.D220,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb56, 16)> 7 THEN COALESCE(dd.D221,  0) ELSE 0 END + CASE WHEN MOD(pb.pb56, 8) > 3 THEN COALESCE(dd.D222,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb56, 4) > 1 THEN COALESCE(dd.D223,  0) ELSE 0 END + CASE WHEN MOD(pb.pb56, 2) = 1 THEN COALESCE(dd.D224,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb57, 16)> 7 THEN COALESCE(dd.D225,  0) ELSE 0 END + CASE WHEN MOD(pb.pb57, 8) > 3 THEN COALESCE(dd.D226,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb57, 4) > 1 THEN COALESCE(dd.D227,  0) ELSE 0 END + CASE WHEN MOD(pb.pb57, 2) = 1 THEN COALESCE(dd.D228,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb58, 16)> 7 THEN COALESCE(dd.D229,  0) ELSE 0 END + CASE WHEN MOD(pb.pb58, 8) > 3 THEN COALESCE(dd.D230,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb58, 4) > 1 THEN COALESCE(dd.D231,  0) ELSE 0 END + CASE WHEN MOD(pb.pb58, 2) = 1 THEN COALESCE(dd.D232,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb59, 16)> 7 THEN COALESCE(dd.D233,  0) ELSE 0 END + CASE WHEN MOD(pb.pb59, 8) > 3 THEN COALESCE(dd.D234,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb59, 4) > 1 THEN COALESCE(dd.D235,  0) ELSE 0 END + CASE WHEN MOD(pb.pb59, 2) = 1 THEN COALESCE(dd.D236,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb60, 16)> 7 THEN COALESCE(dd.D237,  0) ELSE 0 END + CASE WHEN MOD(pb.pb60, 8) > 3 THEN COALESCE(dd.D238,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb60, 4) > 1 THEN COALESCE(dd.D239,  0) ELSE 0 END + CASE WHEN MOD(pb.pb60, 2) = 1 THEN COALESCE(dd.D240,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb61, 16)> 7 THEN COALESCE(dd.D241,  0) ELSE 0 END + CASE WHEN MOD(pb.pb61, 8) > 3 THEN COALESCE(dd.D242,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb61, 4) > 1 THEN COALESCE(dd.D243,  0) ELSE 0 END + CASE WHEN MOD(pb.pb61, 2) = 1 THEN COALESCE(dd.D244,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb62, 16)> 7 THEN COALESCE(dd.D245,  0) ELSE 0 END + CASE WHEN MOD(pb.pb62, 8) > 3 THEN COALESCE(dd.D246,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb62, 4) > 1 THEN COALESCE(dd.D247,  0) ELSE 0 END + CASE WHEN MOD(pb.pb62, 2) = 1 THEN COALESCE(dd.D248,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb63, 16)> 7 THEN COALESCE(dd.D249,  0) ELSE 0 END + CASE WHEN MOD(pb.pb63, 8) > 3 THEN COALESCE(dd.D250,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb63, 4) > 1 THEN COALESCE(dd.D251,  0) ELSE 0 END + CASE WHEN MOD(pb.pb63, 2) = 1 THEN COALESCE(dd.D252,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb64, 16)> 7 THEN COALESCE(dd.D253,  0) ELSE 0 END + CASE WHEN MOD(pb.pb64, 8) > 3 THEN COALESCE(dd.D254,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb64, 4) > 1 THEN COALESCE(dd.D255,  0) ELSE 0 END + CASE WHEN MOD(pb.pb64, 2) = 1 THEN COALESCE(dd.D256,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb65, 16)> 7 THEN COALESCE(dd.D257,  0) ELSE 0 END + CASE WHEN MOD(pb.pb65, 8) > 3 THEN COALESCE(dd.D258,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb65, 4) > 1 THEN COALESCE(dd.D259,  0) ELSE 0 END + CASE WHEN MOD(pb.pb65, 2) = 1 THEN COALESCE(dd.D260,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb66, 16)> 7 THEN COALESCE(dd.D261,  0) ELSE 0 END + CASE WHEN MOD(pb.pb66, 8) > 3 THEN COALESCE(dd.D262,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb66, 4) > 1 THEN COALESCE(dd.D263,  0) ELSE 0 END + CASE WHEN MOD(pb.pb66, 2) = 1 THEN COALESCE(dd.D264,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb67, 16)> 7 THEN COALESCE(dd.D265,  0) ELSE 0 END + CASE WHEN MOD(pb.pb67, 8) > 3 THEN COALESCE(dd.D266,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb67, 4) > 1 THEN COALESCE(dd.D267,  0) ELSE 0 END + CASE WHEN MOD(pb.pb67, 2) = 1 THEN COALESCE(dd.D268,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb68, 16)> 7 THEN COALESCE(dd.D269,  0) ELSE 0 END + CASE WHEN MOD(pb.pb68, 8) > 3 THEN COALESCE(dd.D270,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb68, 4) > 1 THEN COALESCE(dd.D271,  0) ELSE 0 END + CASE WHEN MOD(pb.pb68, 2) = 1 THEN COALESCE(dd.D272,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb69, 16)> 7 THEN COALESCE(dd.D273,  0) ELSE 0 END + CASE WHEN MOD(pb.pb69, 8) > 3 THEN COALESCE(dd.D274,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb69, 4) > 1 THEN COALESCE(dd.D275,  0) ELSE 0 END + CASE WHEN MOD(pb.pb69, 2) = 1 THEN COALESCE(dd.D276,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb70, 16)> 7 THEN COALESCE(dd.D277,  0) ELSE 0 END + CASE WHEN MOD(pb.pb70, 8) > 3 THEN COALESCE(dd.D278,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb70, 4) > 1 THEN COALESCE(dd.D279,  0) ELSE 0 END + CASE WHEN MOD(pb.pb70, 2) = 1 THEN COALESCE(dd.D280,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb71, 16)> 7 THEN COALESCE(dd.D281,  0) ELSE 0 END + CASE WHEN MOD(pb.pb71, 8) > 3 THEN COALESCE(dd.D282,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb71, 4) > 1 THEN COALESCE(dd.D283,  0) ELSE 0 END + CASE WHEN MOD(pb.pb71, 2) = 1 THEN COALESCE(dd.D284,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb72, 16)> 7 THEN COALESCE(dd.D285,  0) ELSE 0 END + CASE WHEN MOD(pb.pb72, 8) > 3 THEN COALESCE(dd.D286,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb72, 4) > 1 THEN COALESCE(dd.D287,  0) ELSE 0 END + CASE WHEN MOD(pb.pb72, 2) = 1 THEN COALESCE(dd.D288,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb73, 16)> 7 THEN COALESCE(dd.D289,  0) ELSE 0 END + CASE WHEN MOD(pb.pb73, 8) > 3 THEN COALESCE(dd.D290,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb73, 4) > 1 THEN COALESCE(dd.D291,  0) ELSE 0 END + CASE WHEN MOD(pb.pb73, 2) = 1 THEN COALESCE(dd.D292,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb74, 16)> 7 THEN COALESCE(dd.D293,  0) ELSE 0 END + CASE WHEN MOD(pb.pb74, 8) > 3 THEN COALESCE(dd.D294,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb74, 4) > 1 THEN COALESCE(dd.D295,  0) ELSE 0 END + CASE WHEN MOD(pb.pb74, 2) = 1 THEN COALESCE(dd.D296,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb75, 16)> 7 THEN COALESCE(dd.D297,  0) ELSE 0 END + CASE WHEN MOD(pb.pb75, 8) > 3 THEN COALESCE(dd.D298,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb75, 4) > 1 THEN COALESCE(dd.D299,  0) ELSE 0 END + CASE WHEN MOD(pb.pb75, 2) = 1 THEN COALESCE(dd.D300,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb76, 16)> 7 THEN COALESCE(dd.D301,  0) ELSE 0 END + CASE WHEN MOD(pb.pb76, 8) > 3 THEN COALESCE(dd.D302,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb76, 4) > 1 THEN COALESCE(dd.D303,  0) ELSE 0 END + CASE WHEN MOD(pb.pb76, 2) = 1 THEN COALESCE(dd.D304,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb77, 16)> 7 THEN COALESCE(dd.D305,  0) ELSE 0 END + CASE WHEN MOD(pb.pb77, 8) > 3 THEN COALESCE(dd.D306,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb77, 4) > 1 THEN COALESCE(dd.D307,  0) ELSE 0 END + CASE WHEN MOD(pb.pb77, 2) = 1 THEN COALESCE(dd.D308,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb78, 16)> 7 THEN COALESCE(dd.D309,  0) ELSE 0 END + CASE WHEN MOD(pb.pb78, 8) > 3 THEN COALESCE(dd.D310,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb78, 4) > 1 THEN COALESCE(dd.D311,  0) ELSE 0 END + CASE WHEN MOD(pb.pb78, 2) = 1 THEN COALESCE(dd.D312,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb79, 16)> 7 THEN COALESCE(dd.D313,  0) ELSE 0 END + CASE WHEN MOD(pb.pb79, 8) > 3 THEN COALESCE(dd.D314,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb79, 4) > 1 THEN COALESCE(dd.D315,  0) ELSE 0 END + CASE WHEN MOD(pb.pb79, 2) = 1 THEN COALESCE(dd.D316,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb80, 16)> 7 THEN COALESCE(dd.D317,  0) ELSE 0 END + CASE WHEN MOD(pb.pb80, 8) > 3 THEN COALESCE(dd.D318,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb80, 4) > 1 THEN COALESCE(dd.D319,  0) ELSE 0 END + CASE WHEN MOD(pb.pb80, 2) = 1 THEN COALESCE(dd.D320,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb81, 16)> 7 THEN COALESCE(dd.D321,  0) ELSE 0 END + CASE WHEN MOD(pb.pb81, 8) > 3 THEN COALESCE(dd.D322,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb81, 4) > 1 THEN COALESCE(dd.D323,  0) ELSE 0 END + CASE WHEN MOD(pb.pb81, 2) = 1 THEN COALESCE(dd.D324,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb82, 16)> 7 THEN COALESCE(dd.D325,  0) ELSE 0 END + CASE WHEN MOD(pb.pb82, 8) > 3 THEN COALESCE(dd.D326,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb82, 4) > 1 THEN COALESCE(dd.D327,  0) ELSE 0 END + CASE WHEN MOD(pb.pb82, 2) = 1 THEN COALESCE(dd.D328,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb83, 16)> 7 THEN COALESCE(dd.D329,  0) ELSE 0 END + CASE WHEN MOD(pb.pb83, 8) > 3 THEN COALESCE(dd.D330,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb83, 4) > 1 THEN COALESCE(dd.D331,  0) ELSE 0 END + CASE WHEN MOD(pb.pb83, 2) = 1 THEN COALESCE(dd.D332,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb84, 16)> 7 THEN COALESCE(dd.D333,  0) ELSE 0 END + CASE WHEN MOD(pb.pb84, 8) > 3 THEN COALESCE(dd.D334,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb84, 4) > 1 THEN COALESCE(dd.D335,  0) ELSE 0 END + CASE WHEN MOD(pb.pb84, 2) = 1 THEN COALESCE(dd.D336,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb85, 16)> 7 THEN COALESCE(dd.D337,  0) ELSE 0 END + CASE WHEN MOD(pb.pb85, 8) > 3 THEN COALESCE(dd.D338,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb85, 4) > 1 THEN COALESCE(dd.D339,  0) ELSE 0 END + CASE WHEN MOD(pb.pb85, 2) = 1 THEN COALESCE(dd.D340,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb86, 16)> 7 THEN COALESCE(dd.D341,  0) ELSE 0 END + CASE WHEN MOD(pb.pb86, 8) > 3 THEN COALESCE(dd.D342,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb86, 4) > 1 THEN COALESCE(dd.D343,  0) ELSE 0 END + CASE WHEN MOD(pb.pb86, 2) = 1 THEN COALESCE(dd.D344,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb87, 16)> 7 THEN COALESCE(dd.D345,  0) ELSE 0 END + CASE WHEN MOD(pb.pb87, 8) > 3 THEN COALESCE(dd.D346,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb87, 4) > 1 THEN COALESCE(dd.D347,  0) ELSE 0 END + CASE WHEN MOD(pb.pb87, 2) = 1 THEN COALESCE(dd.D348,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb88, 16)> 7 THEN COALESCE(dd.D349,  0) ELSE 0 END + CASE WHEN MOD(pb.pb88, 8) > 3 THEN COALESCE(dd.D350,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb88, 4) > 1 THEN COALESCE(dd.D351,  0) ELSE 0 END + CASE WHEN MOD(pb.pb88, 2) = 1 THEN COALESCE(dd.D352,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb89, 16)> 7 THEN COALESCE(dd.D353,  0) ELSE 0 END + CASE WHEN MOD(pb.pb89, 8) > 3 THEN COALESCE(dd.D354,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb89, 4) > 1 THEN COALESCE(dd.D355,  0) ELSE 0 END + CASE WHEN MOD(pb.pb89, 2) = 1 THEN COALESCE(dd.D356,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb90, 16)> 7 THEN COALESCE(dd.D357,  0) ELSE 0 END + CASE WHEN MOD(pb.pb90, 8) > 3 THEN COALESCE(dd.D358,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb90, 4) > 1 THEN COALESCE(dd.D359,  0) ELSE 0 END + CASE WHEN MOD(pb.pb90, 2) = 1 THEN COALESCE(dd.D360,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb91, 16)> 7 THEN COALESCE(dd.D361,  0) ELSE 0 END + CASE WHEN MOD(pb.pb91, 8) > 3 THEN COALESCE(dd.D362,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb91, 4) > 1 THEN COALESCE(dd.D363,  0) ELSE 0 END + CASE WHEN MOD(pb.pb91, 2) = 1 THEN COALESCE(dd.D364,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb92, 16)> 7 THEN COALESCE(dd.D365,  0) ELSE 0 END + CASE WHEN MOD(pb.pb92, 8) > 3 THEN COALESCE(dd.D366,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb92, 4) > 1 THEN COALESCE(dd.D367,  0) ELSE 0 END + CASE WHEN MOD(pb.pb92, 2) = 1 THEN COALESCE(dd.D368,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb93, 16)> 7 THEN COALESCE(dd.D369,  0) ELSE 0 END + CASE WHEN MOD(pb.pb93, 8) > 3 THEN COALESCE(dd.D370,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb93, 4) > 1 THEN COALESCE(dd.D371,  0) ELSE 0 END + CASE WHEN MOD(pb.pb93, 2) = 1 THEN COALESCE(dd.D372,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb94, 16)> 7 THEN COALESCE(dd.D373,  0) ELSE 0 END + CASE WHEN MOD(pb.pb94, 8) > 3 THEN COALESCE(dd.D374,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb94, 4) > 1 THEN COALESCE(dd.D375,  0) ELSE 0 END + CASE WHEN MOD(pb.pb94, 2) = 1 THEN COALESCE(dd.D376,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb95, 16)> 7 THEN COALESCE(dd.D377,  0) ELSE 0 END + CASE WHEN MOD(pb.pb95, 8) > 3 THEN COALESCE(dd.D378,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb95, 4) > 1 THEN COALESCE(dd.D379,  0) ELSE 0 END + CASE WHEN MOD(pb.pb95, 2) = 1 THEN COALESCE(dd.D380,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb96, 16)> 7 THEN COALESCE(dd.D381,  0) ELSE 0 END + CASE WHEN MOD(pb.pb96, 8) > 3 THEN COALESCE(dd.D382,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb96, 4) > 1 THEN COALESCE(dd.D383,  0) ELSE 0 END + CASE WHEN MOD(pb.pb96, 2) = 1 THEN COALESCE(dd.D384,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb97, 16)> 7 THEN COALESCE(dd.D385,  0) ELSE 0 END + CASE WHEN MOD(pb.pb97, 8) > 3 THEN COALESCE(dd.D386,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb97, 4) > 1 THEN COALESCE(dd.D387,  0) ELSE 0 END + CASE WHEN MOD(pb.pb97, 2) = 1 THEN COALESCE(dd.D388,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb98, 16)> 7 THEN COALESCE(dd.D389,  0) ELSE 0 END + CASE WHEN MOD(pb.pb98, 8) > 3 THEN COALESCE(dd.D390,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb98, 4) > 1 THEN COALESCE(dd.D391,  0) ELSE 0 END + CASE WHEN MOD(pb.pb98, 2) = 1 THEN COALESCE(dd.D392,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb99, 16)> 7 THEN COALESCE(dd.D393,  0) ELSE 0 END + CASE WHEN MOD(pb.pb99, 8) > 3 THEN COALESCE(dd.D394,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb99, 4) > 1 THEN COALESCE(dd.D395,  0) ELSE 0 END + CASE WHEN MOD(pb.pb99, 2) = 1 THEN COALESCE(dd.D396,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb100, 16)> 7 THEN COALESCE(dd.D397,  0) ELSE 0 END + CASE WHEN MOD(pb.pb100, 8) > 3 THEN COALESCE(dd.D398,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb100, 4) > 1 THEN COALESCE(dd.D399,  0) ELSE 0 END + CASE WHEN MOD(pb.pb100, 2) = 1 THEN COALESCE(dd.D400,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb101, 16)> 7 THEN COALESCE(dd.D401,  0) ELSE 0 END + CASE WHEN MOD(pb.pb101, 8) > 3 THEN COALESCE(dd.D402,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb101, 4) > 1 THEN COALESCE(dd.D403,  0) ELSE 0 END + CASE WHEN MOD(pb.pb101, 2) = 1 THEN COALESCE(dd.D404,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb102, 16)> 7 THEN COALESCE(dd.D405,  0) ELSE 0 END + CASE WHEN MOD(pb.pb102, 8) > 3 THEN COALESCE(dd.D406,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb102, 4) > 1 THEN COALESCE(dd.D407,  0) ELSE 0 END + CASE WHEN MOD(pb.pb102, 2) = 1 THEN COALESCE(dd.D408,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb103, 16)> 7 THEN COALESCE(dd.D409,  0) ELSE 0 END + CASE WHEN MOD(pb.pb103, 8) > 3 THEN COALESCE(dd.D410,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb103, 4) > 1 THEN COALESCE(dd.D411,  0) ELSE 0 END + CASE WHEN MOD(pb.pb103, 2) = 1 THEN COALESCE(dd.D412,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb104, 16)> 7 THEN COALESCE(dd.D413,  0) ELSE 0 END + CASE WHEN MOD(pb.pb104, 8) > 3 THEN COALESCE(dd.D414,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb104, 4) > 1 THEN COALESCE(dd.D415,  0) ELSE 0 END + CASE WHEN MOD(pb.pb104, 2) = 1 THEN COALESCE(dd.D416,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb105, 16)> 7 THEN COALESCE(dd.D417,  0) ELSE 0 END + CASE WHEN MOD(pb.pb105, 8) > 3 THEN COALESCE(dd.D418,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb105, 4) > 1 THEN COALESCE(dd.D419,  0) ELSE 0 END + CASE WHEN MOD(pb.pb105, 2) = 1 THEN COALESCE(dd.D420,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb106, 16)> 7 THEN COALESCE(dd.D421,  0) ELSE 0 END + CASE WHEN MOD(pb.pb106, 8) > 3 THEN COALESCE(dd.D422,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb106, 4) > 1 THEN COALESCE(dd.D423,  0) ELSE 0 END + CASE WHEN MOD(pb.pb106, 2) = 1 THEN COALESCE(dd.D424,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb107, 16)> 7 THEN COALESCE(dd.D425,  0) ELSE 0 END + CASE WHEN MOD(pb.pb107, 8) > 3 THEN COALESCE(dd.D426,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb107, 4) > 1 THEN COALESCE(dd.D427,  0) ELSE 0 END + CASE WHEN MOD(pb.pb107, 2) = 1 THEN COALESCE(dd.D428,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb108, 16)> 7 THEN COALESCE(dd.D429,  0) ELSE 0 END + CASE WHEN MOD(pb.pb108, 8) > 3 THEN COALESCE(dd.D430,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb108, 4) > 1 THEN COALESCE(dd.D431,  0) ELSE 0 END + CASE WHEN MOD(pb.pb108, 2) = 1 THEN COALESCE(dd.D432,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb109, 16)> 7 THEN COALESCE(dd.D433,  0) ELSE 0 END + CASE WHEN MOD(pb.pb109, 8) > 3 THEN COALESCE(dd.D434,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb109, 4) > 1 THEN COALESCE(dd.D435,  0) ELSE 0 END + CASE WHEN MOD(pb.pb109, 2) = 1 THEN COALESCE(dd.D436,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb110, 16)> 7 THEN COALESCE(dd.D437,  0) ELSE 0 END + CASE WHEN MOD(pb.pb110, 8) > 3 THEN COALESCE(dd.D438,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb110, 4) > 1 THEN COALESCE(dd.D439,  0) ELSE 0 END + CASE WHEN MOD(pb.pb110, 2) = 1 THEN COALESCE(dd.D440,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb111, 16)> 7 THEN COALESCE(dd.D441,  0) ELSE 0 END + CASE WHEN MOD(pb.pb111, 8) > 3 THEN COALESCE(dd.D442,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb111, 4) > 1 THEN COALESCE(dd.D443,  0) ELSE 0 END + CASE WHEN MOD(pb.pb111, 2) = 1 THEN COALESCE(dd.D444,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb112, 16)> 7 THEN COALESCE(dd.D445,  0) ELSE 0 END + CASE WHEN MOD(pb.pb112, 8) > 3 THEN COALESCE(dd.D446,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb112, 4) > 1 THEN COALESCE(dd.D447,  0) ELSE 0 END + CASE WHEN MOD(pb.pb112, 2) = 1 THEN COALESCE(dd.D448,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb113, 16)> 7 THEN COALESCE(dd.D449,  0) ELSE 0 END + CASE WHEN MOD(pb.pb113, 8) > 3 THEN COALESCE(dd.D450,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb113, 4) > 1 THEN COALESCE(dd.D451,  0) ELSE 0 END + CASE WHEN MOD(pb.pb113, 2) = 1 THEN COALESCE(dd.D452,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb114, 16)> 7 THEN COALESCE(dd.D453,  0) ELSE 0 END + CASE WHEN MOD(pb.pb114, 8) > 3 THEN COALESCE(dd.D454,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb114, 4) > 1 THEN COALESCE(dd.D455,  0) ELSE 0 END + CASE WHEN MOD(pb.pb114, 2) = 1 THEN COALESCE(dd.D456,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb115, 16)> 7 THEN COALESCE(dd.D457,  0) ELSE 0 END + CASE WHEN MOD(pb.pb115, 8) > 3 THEN COALESCE(dd.D458,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb115, 4) > 1 THEN COALESCE(dd.D459,  0) ELSE 0 END + CASE WHEN MOD(pb.pb115, 2) = 1 THEN COALESCE(dd.D460,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb116, 16)> 7 THEN COALESCE(dd.D461,  0) ELSE 0 END + CASE WHEN MOD(pb.pb116, 8) > 3 THEN COALESCE(dd.D462,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb116, 4) > 1 THEN COALESCE(dd.D463,  0) ELSE 0 END + CASE WHEN MOD(pb.pb116, 2) = 1 THEN COALESCE(dd.D464,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb117, 16)> 7 THEN COALESCE(dd.D465,  0) ELSE 0 END + CASE WHEN MOD(pb.pb117, 8) > 3 THEN COALESCE(dd.D466,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb117, 4) > 1 THEN COALESCE(dd.D467,  0) ELSE 0 END + CASE WHEN MOD(pb.pb117, 2) = 1 THEN COALESCE(dd.D468,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb118, 16)> 7 THEN COALESCE(dd.D469,  0) ELSE 0 END + CASE WHEN MOD(pb.pb118, 8) > 3 THEN COALESCE(dd.D470,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb118, 4) > 1 THEN COALESCE(dd.D471,  0) ELSE 0 END + CASE WHEN MOD(pb.pb118, 2) = 1 THEN COALESCE(dd.D472,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb119, 16)> 7 THEN COALESCE(dd.D473,  0) ELSE 0 END + CASE WHEN MOD(pb.pb119, 8) > 3 THEN COALESCE(dd.D474,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb119, 4) > 1 THEN COALESCE(dd.D475,  0) ELSE 0 END + CASE WHEN MOD(pb.pb119, 2) = 1 THEN COALESCE(dd.D476,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb120, 16)> 7 THEN COALESCE(dd.D477,  0) ELSE 0 END + CASE WHEN MOD(pb.pb120, 8) > 3 THEN COALESCE(dd.D478,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb120, 4) > 1 THEN COALESCE(dd.D479,  0) ELSE 0 END + CASE WHEN MOD(pb.pb120, 2) = 1 THEN COALESCE(dd.D480,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb121, 16)> 7 THEN COALESCE(dd.D481,  0) ELSE 0 END + CASE WHEN MOD(pb.pb121, 8) > 3 THEN COALESCE(dd.D482,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb121, 4) > 1 THEN COALESCE(dd.D483,  0) ELSE 0 END + CASE WHEN MOD(pb.pb121, 2) = 1 THEN COALESCE(dd.D484,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb122, 16)> 7 THEN COALESCE(dd.D485,  0) ELSE 0 END + CASE WHEN MOD(pb.pb122, 8) > 3 THEN COALESCE(dd.D486,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb122, 4) > 1 THEN COALESCE(dd.D487,  0) ELSE 0 END + CASE WHEN MOD(pb.pb122, 2) = 1 THEN COALESCE(dd.D488,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb123, 16)> 7 THEN COALESCE(dd.D489,  0) ELSE 0 END + CASE WHEN MOD(pb.pb123, 8) > 3 THEN COALESCE(dd.D490,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb123, 4) > 1 THEN COALESCE(dd.D491,  0) ELSE 0 END + CASE WHEN MOD(pb.pb123, 2) = 1 THEN COALESCE(dd.D492,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb124, 16)> 7 THEN COALESCE(dd.D493,  0) ELSE 0 END + CASE WHEN MOD(pb.pb124, 8) > 3 THEN COALESCE(dd.D494,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb124, 4) > 1 THEN COALESCE(dd.D495,  0) ELSE 0 END + CASE WHEN MOD(pb.pb124, 2) = 1 THEN COALESCE(dd.D496,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb125, 16)> 7 THEN COALESCE(dd.D497,  0) ELSE 0 END + CASE WHEN MOD(pb.pb125, 8) > 3 THEN COALESCE(dd.D498,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb125, 4) > 1 THEN COALESCE(dd.D499,  0) ELSE 0 END + CASE WHEN MOD(pb.pb125, 2) = 1 THEN COALESCE(dd.D500,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb126, 16)> 7 THEN COALESCE(dd.D501,  0) ELSE 0 END + CASE WHEN MOD(pb.pb126, 8) > 3 THEN COALESCE(dd.D502,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb126, 4) > 1 THEN COALESCE(dd.D503,  0) ELSE 0 END + CASE WHEN MOD(pb.pb126, 2) = 1 THEN COALESCE(dd.D504,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb127, 16)> 7 THEN COALESCE(dd.D505,  0) ELSE 0 END + CASE WHEN MOD(pb.pb127, 8) > 3 THEN COALESCE(dd.D506,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb127, 4) > 1 THEN COALESCE(dd.D507,  0) ELSE 0 END + CASE WHEN MOD(pb.pb127, 2) = 1 THEN COALESCE(dd.D508,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb128, 16)> 7 THEN COALESCE(dd.D509,  0) ELSE 0 END + CASE WHEN MOD(pb.pb128, 8) > 3 THEN COALESCE(dd.D510,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb128, 4) > 1 THEN COALESCE(dd.D511,  0) ELSE 0 END + CASE WHEN MOD(pb.pb128, 2) = 1 THEN COALESCE(dd.D512,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb129, 16)> 7 THEN COALESCE(dd.D513,  0) ELSE 0 END + CASE WHEN MOD(pb.pb129, 8) > 3 THEN COALESCE(dd.D514,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb129, 4) > 1 THEN COALESCE(dd.D515,  0) ELSE 0 END + CASE WHEN MOD(pb.pb129, 2) = 1 THEN COALESCE(dd.D516,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb130, 16)> 7 THEN COALESCE(dd.D517,  0) ELSE 0 END + CASE WHEN MOD(pb.pb130, 8) > 3 THEN COALESCE(dd.D518,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb130, 4) > 1 THEN COALESCE(dd.D519,  0) ELSE 0 END + CASE WHEN MOD(pb.pb130, 2) = 1 THEN COALESCE(dd.D520,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb131, 16)> 7 THEN COALESCE(dd.D521,  0) ELSE 0 END + CASE WHEN MOD(pb.pb131, 8) > 3 THEN COALESCE(dd.D522,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb131, 4) > 1 THEN COALESCE(dd.D523,  0) ELSE 0 END + CASE WHEN MOD(pb.pb131, 2) = 1 THEN COALESCE(dd.D524,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb132, 16)> 7 THEN COALESCE(dd.D525,  0) ELSE 0 END + CASE WHEN MOD(pb.pb132, 8) > 3 THEN COALESCE(dd.D526,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb132, 4) > 1 THEN COALESCE(dd.D527,  0) ELSE 0 END + CASE WHEN MOD(pb.pb132, 2) = 1 THEN COALESCE(dd.D528,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb133, 16)> 7 THEN COALESCE(dd.D529,  0) ELSE 0 END + CASE WHEN MOD(pb.pb133, 8) > 3 THEN COALESCE(dd.D530,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb133, 4) > 1 THEN COALESCE(dd.D531,  0) ELSE 0 END + CASE WHEN MOD(pb.pb133, 2) = 1 THEN COALESCE(dd.D532,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb134, 16)> 7 THEN COALESCE(dd.D533,  0) ELSE 0 END + CASE WHEN MOD(pb.pb134, 8) > 3 THEN COALESCE(dd.D534,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb134, 4) > 1 THEN COALESCE(dd.D535,  0) ELSE 0 END + CASE WHEN MOD(pb.pb134, 2) = 1 THEN COALESCE(dd.D536,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb135, 16)> 7 THEN COALESCE(dd.D537,  0) ELSE 0 END + CASE WHEN MOD(pb.pb135, 8) > 3 THEN COALESCE(dd.D538,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb135, 4) > 1 THEN COALESCE(dd.D539,  0) ELSE 0 END + CASE WHEN MOD(pb.pb135, 2) = 1 THEN COALESCE(dd.D540,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb136, 16)> 7 THEN COALESCE(dd.D541,  0) ELSE 0 END + CASE WHEN MOD(pb.pb136, 8) > 3 THEN COALESCE(dd.D542,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb136, 4) > 1 THEN COALESCE(dd.D543,  0) ELSE 0 END + CASE WHEN MOD(pb.pb136, 2) = 1 THEN COALESCE(dd.D544,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb137, 16)> 7 THEN COALESCE(dd.D545,  0) ELSE 0 END + CASE WHEN MOD(pb.pb137, 8) > 3 THEN COALESCE(dd.D546,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb137, 4) > 1 THEN COALESCE(dd.D547,  0) ELSE 0 END + CASE WHEN MOD(pb.pb137, 2) = 1 THEN COALESCE(dd.D548,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb138, 16)> 7 THEN COALESCE(dd.D549,  0) ELSE 0 END + CASE WHEN MOD(pb.pb138, 8) > 3 THEN COALESCE(dd.D550,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb138, 4) > 1 THEN COALESCE(dd.D551,  0) ELSE 0 END + CASE WHEN MOD(pb.pb138, 2) = 1 THEN COALESCE(dd.D552,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb139, 16)> 7 THEN COALESCE(dd.D553,  0) ELSE 0 END + CASE WHEN MOD(pb.pb139, 8) > 3 THEN COALESCE(dd.D554,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb139, 4) > 1 THEN COALESCE(dd.D555,  0) ELSE 0 END + CASE WHEN MOD(pb.pb139, 2) = 1 THEN COALESCE(dd.D556,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb140, 16)> 7 THEN COALESCE(dd.D557,  0) ELSE 0 END + CASE WHEN MOD(pb.pb140, 8) > 3 THEN COALESCE(dd.D558,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb140, 4) > 1 THEN COALESCE(dd.D559,  0) ELSE 0 END + CASE WHEN MOD(pb.pb140, 2) = 1 THEN COALESCE(dd.D560,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb141, 16)> 7 THEN COALESCE(dd.D561,  0) ELSE 0 END + CASE WHEN MOD(pb.pb141, 8) > 3 THEN COALESCE(dd.D562,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb141, 4) > 1 THEN COALESCE(dd.D563,  0) ELSE 0 END + CASE WHEN MOD(pb.pb141, 2) = 1 THEN COALESCE(dd.D564,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb142, 16)> 7 THEN COALESCE(dd.D565,  0) ELSE 0 END + CASE WHEN MOD(pb.pb142, 8) > 3 THEN COALESCE(dd.D566,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb142, 4) > 1 THEN COALESCE(dd.D567,  0) ELSE 0 END + CASE WHEN MOD(pb.pb142, 2) = 1 THEN COALESCE(dd.D568,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb143, 16)> 7 THEN COALESCE(dd.D569,  0) ELSE 0 END + CASE WHEN MOD(pb.pb143, 8) > 3 THEN COALESCE(dd.D570,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb143, 4) > 1 THEN COALESCE(dd.D571,  0) ELSE 0 END + CASE WHEN MOD(pb.pb143, 2) = 1 THEN COALESCE(dd.D572,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb144, 16)> 7 THEN COALESCE(dd.D573,  0) ELSE 0 END + CASE WHEN MOD(pb.pb144, 8) > 3 THEN COALESCE(dd.D574,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb144, 4) > 1 THEN COALESCE(dd.D575,  0) ELSE 0 END + CASE WHEN MOD(pb.pb144, 2) = 1 THEN COALESCE(dd.D576,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb145, 16)> 7 THEN COALESCE(dd.D577,  0) ELSE 0 END + CASE WHEN MOD(pb.pb145, 8) > 3 THEN COALESCE(dd.D578,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb145, 4) > 1 THEN COALESCE(dd.D579,  0) ELSE 0 END + CASE WHEN MOD(pb.pb145, 2) = 1 THEN COALESCE(dd.D580,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb146, 16)> 7 THEN COALESCE(dd.D581,  0) ELSE 0 END + CASE WHEN MOD(pb.pb146, 8) > 3 THEN COALESCE(dd.D582,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb146, 4) > 1 THEN COALESCE(dd.D583,  0) ELSE 0 END + CASE WHEN MOD(pb.pb146, 2) = 1 THEN COALESCE(dd.D584,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb147, 16)> 7 THEN COALESCE(dd.D585,  0) ELSE 0 END + CASE WHEN MOD(pb.pb147, 8) > 3 THEN COALESCE(dd.D586,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb147, 4) > 1 THEN COALESCE(dd.D587,  0) ELSE 0 END + CASE WHEN MOD(pb.pb147, 2) = 1 THEN COALESCE(dd.D588,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb148, 16)> 7 THEN COALESCE(dd.D589,  0) ELSE 0 END + CASE WHEN MOD(pb.pb148, 8) > 3 THEN COALESCE(dd.D590,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb148, 4) > 1 THEN COALESCE(dd.D591,  0) ELSE 0 END + CASE WHEN MOD(pb.pb148, 2) = 1 THEN COALESCE(dd.D592,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb149, 16)> 7 THEN COALESCE(dd.D593,  0) ELSE 0 END + CASE WHEN MOD(pb.pb149, 8) > 3 THEN COALESCE(dd.D594,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb149, 4) > 1 THEN COALESCE(dd.D595,  0) ELSE 0 END + CASE WHEN MOD(pb.pb149, 2) = 1 THEN COALESCE(dd.D596,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb150, 16)> 7 THEN COALESCE(dd.D597,  0) ELSE 0 END + CASE WHEN MOD(pb.pb150, 8) > 3 THEN COALESCE(dd.D598,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb150, 4) > 1 THEN COALESCE(dd.D599,  0) ELSE 0 END + CASE WHEN MOD(pb.pb150, 2) = 1 THEN COALESCE(dd.D600,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb151, 16)> 7 THEN COALESCE(dd.D601,  0) ELSE 0 END + CASE WHEN MOD(pb.pb151, 8) > 3 THEN COALESCE(dd.D602,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb151, 4) > 1 THEN COALESCE(dd.D603,  0) ELSE 0 END + CASE WHEN MOD(pb.pb151, 2) = 1 THEN COALESCE(dd.D604,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb152, 16)> 7 THEN COALESCE(dd.D605,  0) ELSE 0 END + CASE WHEN MOD(pb.pb152, 8) > 3 THEN COALESCE(dd.D606,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb152, 4) > 1 THEN COALESCE(dd.D607,  0) ELSE 0 END + CASE WHEN MOD(pb.pb152, 2) = 1 THEN COALESCE(dd.D608,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb153, 16)> 7 THEN COALESCE(dd.D609,  0) ELSE 0 END + CASE WHEN MOD(pb.pb153, 8) > 3 THEN COALESCE(dd.D610,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb153, 4) > 1 THEN COALESCE(dd.D611,  0) ELSE 0 END + CASE WHEN MOD(pb.pb153, 2) = 1 THEN COALESCE(dd.D612,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb154, 16)> 7 THEN COALESCE(dd.D613,  0) ELSE 0 END + CASE WHEN MOD(pb.pb154, 8) > 3 THEN COALESCE(dd.D614,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb154, 4) > 1 THEN COALESCE(dd.D615,  0) ELSE 0 END + CASE WHEN MOD(pb.pb154, 2) = 1 THEN COALESCE(dd.D616,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb155, 16)> 7 THEN COALESCE(dd.D617,  0) ELSE 0 END + CASE WHEN MOD(pb.pb155, 8) > 3 THEN COALESCE(dd.D618,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb155, 4) > 1 THEN COALESCE(dd.D619,  0) ELSE 0 END + CASE WHEN MOD(pb.pb155, 2) = 1 THEN COALESCE(dd.D620,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb156, 16)> 7 THEN COALESCE(dd.D621,  0) ELSE 0 END + CASE WHEN MOD(pb.pb156, 8) > 3 THEN COALESCE(dd.D622,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb156, 4) > 1 THEN COALESCE(dd.D623,  0) ELSE 0 END + CASE WHEN MOD(pb.pb156, 2) = 1 THEN COALESCE(dd.D624,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb157, 16)> 7 THEN COALESCE(dd.D625,  0) ELSE 0 END + CASE WHEN MOD(pb.pb157, 8) > 3 THEN COALESCE(dd.D626,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb157, 4) > 1 THEN COALESCE(dd.D627,  0) ELSE 0 END + CASE WHEN MOD(pb.pb157, 2) = 1 THEN COALESCE(dd.D628,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb158, 16)> 7 THEN COALESCE(dd.D629,  0) ELSE 0 END + CASE WHEN MOD(pb.pb158, 8) > 3 THEN COALESCE(dd.D630,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb158, 4) > 1 THEN COALESCE(dd.D631,  0) ELSE 0 END + CASE WHEN MOD(pb.pb158, 2) = 1 THEN COALESCE(dd.D632,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb159, 16)> 7 THEN COALESCE(dd.D633,  0) ELSE 0 END + CASE WHEN MOD(pb.pb159, 8) > 3 THEN COALESCE(dd.D634,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb159, 4) > 1 THEN COALESCE(dd.D635,  0) ELSE 0 END + CASE WHEN MOD(pb.pb159, 2) = 1 THEN COALESCE(dd.D636,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb160, 16)> 7 THEN COALESCE(dd.D637,  0) ELSE 0 END + CASE WHEN MOD(pb.pb160, 8) > 3 THEN COALESCE(dd.D638,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb160, 4) > 1 THEN COALESCE(dd.D639,  0) ELSE 0 END + CASE WHEN MOD(pb.pb160, 2) = 1 THEN COALESCE(dd.D640,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb161, 16)> 7 THEN COALESCE(dd.D641,  0) ELSE 0 END + CASE WHEN MOD(pb.pb161, 8) > 3 THEN COALESCE(dd.D642,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb161, 4) > 1 THEN COALESCE(dd.D643,  0) ELSE 0 END + CASE WHEN MOD(pb.pb161, 2) = 1 THEN COALESCE(dd.D644,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb162, 16)> 7 THEN COALESCE(dd.D645,  0) ELSE 0 END + CASE WHEN MOD(pb.pb162, 8) > 3 THEN COALESCE(dd.D646,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb162, 4) > 1 THEN COALESCE(dd.D647,  0) ELSE 0 END + CASE WHEN MOD(pb.pb162, 2) = 1 THEN COALESCE(dd.D648,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb163, 16)> 7 THEN COALESCE(dd.D649,  0) ELSE 0 END + CASE WHEN MOD(pb.pb163, 8) > 3 THEN COALESCE(dd.D650,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb163, 4) > 1 THEN COALESCE(dd.D651,  0) ELSE 0 END + CASE WHEN MOD(pb.pb163, 2) = 1 THEN COALESCE(dd.D652,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb164, 16)> 7 THEN COALESCE(dd.D653,  0) ELSE 0 END + CASE WHEN MOD(pb.pb164, 8) > 3 THEN COALESCE(dd.D654,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb164, 4) > 1 THEN COALESCE(dd.D655,  0) ELSE 0 END + CASE WHEN MOD(pb.pb164, 2) = 1 THEN COALESCE(dd.D656,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb165, 16)> 7 THEN COALESCE(dd.D657,  0) ELSE 0 END + CASE WHEN MOD(pb.pb165, 8) > 3 THEN COALESCE(dd.D658,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb165, 4) > 1 THEN COALESCE(dd.D659,  0) ELSE 0 END + CASE WHEN MOD(pb.pb165, 2) = 1 THEN COALESCE(dd.D660,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb166, 16)> 7 THEN COALESCE(dd.D661,  0) ELSE 0 END + CASE WHEN MOD(pb.pb166, 8) > 3 THEN COALESCE(dd.D662,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb166, 4) > 1 THEN COALESCE(dd.D663,  0) ELSE 0 END + CASE WHEN MOD(pb.pb166, 2) = 1 THEN COALESCE(dd.D664,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb167, 16)> 7 THEN COALESCE(dd.D665,  0) ELSE 0 END + CASE WHEN MOD(pb.pb167, 8) > 3 THEN COALESCE(dd.D666,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb167, 4) > 1 THEN COALESCE(dd.D667,  0) ELSE 0 END + CASE WHEN MOD(pb.pb167, 2) = 1 THEN COALESCE(dd.D668,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb168, 16)> 7 THEN COALESCE(dd.D669,  0) ELSE 0 END + CASE WHEN MOD(pb.pb168, 8) > 3 THEN COALESCE(dd.D670,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb168, 4) > 1 THEN COALESCE(dd.D671,  0) ELSE 0 END + CASE WHEN MOD(pb.pb168, 2) = 1 THEN COALESCE(dd.D672,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb169, 16)> 7 THEN COALESCE(dd.D673,  0) ELSE 0 END + CASE WHEN MOD(pb.pb169, 8) > 3 THEN COALESCE(dd.D674,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb169, 4) > 1 THEN COALESCE(dd.D675,  0) ELSE 0 END + CASE WHEN MOD(pb.pb169, 2) = 1 THEN COALESCE(dd.D676,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb170, 16)> 7 THEN COALESCE(dd.D677,  0) ELSE 0 END + CASE WHEN MOD(pb.pb170, 8) > 3 THEN COALESCE(dd.D678,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb170, 4) > 1 THEN COALESCE(dd.D679,  0) ELSE 0 END + CASE WHEN MOD(pb.pb170, 2) = 1 THEN COALESCE(dd.D680,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb171, 16)> 7 THEN COALESCE(dd.D681,  0) ELSE 0 END + CASE WHEN MOD(pb.pb171, 8) > 3 THEN COALESCE(dd.D682,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb171, 4) > 1 THEN COALESCE(dd.D683,  0) ELSE 0 END + CASE WHEN MOD(pb.pb171, 2) = 1 THEN COALESCE(dd.D684,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb172, 16)> 7 THEN COALESCE(dd.D685,  0) ELSE 0 END + CASE WHEN MOD(pb.pb172, 8) > 3 THEN COALESCE(dd.D686,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb172, 4) > 1 THEN COALESCE(dd.D687,  0) ELSE 0 END + CASE WHEN MOD(pb.pb172, 2) = 1 THEN COALESCE(dd.D688,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb173, 16)> 7 THEN COALESCE(dd.D689,  0) ELSE 0 END + CASE WHEN MOD(pb.pb173, 8) > 3 THEN COALESCE(dd.D690,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb173, 4) > 1 THEN COALESCE(dd.D691,  0) ELSE 0 END + CASE WHEN MOD(pb.pb173, 2) = 1 THEN COALESCE(dd.D692,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb174, 16)> 7 THEN COALESCE(dd.D693,  0) ELSE 0 END + CASE WHEN MOD(pb.pb174, 8) > 3 THEN COALESCE(dd.D694,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb174, 4) > 1 THEN COALESCE(dd.D695,  0) ELSE 0 END + CASE WHEN MOD(pb.pb174, 2) = 1 THEN COALESCE(dd.D696,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb175, 16)> 7 THEN COALESCE(dd.D697,  0) ELSE 0 END + CASE WHEN MOD(pb.pb175, 8) > 3 THEN COALESCE(dd.D698,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb175, 4) > 1 THEN COALESCE(dd.D699,  0) ELSE 0 END + CASE WHEN MOD(pb.pb175, 2) = 1 THEN COALESCE(dd.D700,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb176, 16)> 7 THEN COALESCE(dd.D701,  0) ELSE 0 END + CASE WHEN MOD(pb.pb176, 8) > 3 THEN COALESCE(dd.D702,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb176, 4) > 1 THEN COALESCE(dd.D703,  0) ELSE 0 END + CASE WHEN MOD(pb.pb176, 2) = 1 THEN COALESCE(dd.D704,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb177, 16)> 7 THEN COALESCE(dd.D705,  0) ELSE 0 END + CASE WHEN MOD(pb.pb177, 8) > 3 THEN COALESCE(dd.D706,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb177, 4) > 1 THEN COALESCE(dd.D707,  0) ELSE 0 END + CASE WHEN MOD(pb.pb177, 2) = 1 THEN COALESCE(dd.D708,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb178, 16)> 7 THEN COALESCE(dd.D709,  0) ELSE 0 END + CASE WHEN MOD(pb.pb178, 8) > 3 THEN COALESCE(dd.D710,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb178, 4) > 1 THEN COALESCE(dd.D711,  0) ELSE 0 END + CASE WHEN MOD(pb.pb178, 2) = 1 THEN COALESCE(dd.D712,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb179, 16)> 7 THEN COALESCE(dd.D713,  0) ELSE 0 END + CASE WHEN MOD(pb.pb179, 8) > 3 THEN COALESCE(dd.D714,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb179, 4) > 1 THEN COALESCE(dd.D715,  0) ELSE 0 END + CASE WHEN MOD(pb.pb179, 2) = 1 THEN COALESCE(dd.D716,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb180, 16)> 7 THEN COALESCE(dd.D717,  0) ELSE 0 END + CASE WHEN MOD(pb.pb180, 8) > 3 THEN COALESCE(dd.D718,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb180, 4) > 1 THEN COALESCE(dd.D719,  0) ELSE 0 END + CASE WHEN MOD(pb.pb180, 2) = 1 THEN COALESCE(dd.D720,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb181, 16)> 7 THEN COALESCE(dd.D721,  0) ELSE 0 END + CASE WHEN MOD(pb.pb181, 8) > 3 THEN COALESCE(dd.D722,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb181, 4) > 1 THEN COALESCE(dd.D723,  0) ELSE 0 END + CASE WHEN MOD(pb.pb181, 2) = 1 THEN COALESCE(dd.D724,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb182, 16)> 7 THEN COALESCE(dd.D725,  0) ELSE 0 END + CASE WHEN MOD(pb.pb182, 8) > 3 THEN COALESCE(dd.D726,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb182, 4) > 1 THEN COALESCE(dd.D727,  0) ELSE 0 END + CASE WHEN MOD(pb.pb182, 2) = 1 THEN COALESCE(dd.D728,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb183, 16)> 7 THEN COALESCE(dd.D729,  0) ELSE 0 END + CASE WHEN MOD(pb.pb183, 8) > 3 THEN COALESCE(dd.D730,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb183, 4) > 1 THEN COALESCE(dd.D731,  0) ELSE 0 END + CASE WHEN MOD(pb.pb183, 2) = 1 THEN COALESCE(dd.D732,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb184, 16)> 7 THEN COALESCE(dd.D733,  0) ELSE 0 END + CASE WHEN MOD(pb.pb184, 8) > 3 THEN COALESCE(dd.D734,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb184, 4) > 1 THEN COALESCE(dd.D735,  0) ELSE 0 END + CASE WHEN MOD(pb.pb184, 2) = 1 THEN COALESCE(dd.D736,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb185, 16)> 7 THEN COALESCE(dd.D737,  0) ELSE 0 END + CASE WHEN MOD(pb.pb185, 8) > 3 THEN COALESCE(dd.D738,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb185, 4) > 1 THEN COALESCE(dd.D739,  0) ELSE 0 END + CASE WHEN MOD(pb.pb185, 2) = 1 THEN COALESCE(dd.D740,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb186, 16)> 7 THEN COALESCE(dd.D741,  0) ELSE 0 END + CASE WHEN MOD(pb.pb186, 8) > 3 THEN COALESCE(dd.D742,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb186, 4) > 1 THEN COALESCE(dd.D743,  0) ELSE 0 END + CASE WHEN MOD(pb.pb186, 2) = 1 THEN COALESCE(dd.D744,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb187, 16)> 7 THEN COALESCE(dd.D745,  0) ELSE 0 END + CASE WHEN MOD(pb.pb187, 8) > 3 THEN COALESCE(dd.D746,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb187, 4) > 1 THEN COALESCE(dd.D747,  0) ELSE 0 END + CASE WHEN MOD(pb.pb187, 2) = 1 THEN COALESCE(dd.D748,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb188, 16)> 7 THEN COALESCE(dd.D749,  0) ELSE 0 END + CASE WHEN MOD(pb.pb188, 8) > 3 THEN COALESCE(dd.D750,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb188, 4) > 1 THEN COALESCE(dd.D751,  0) ELSE 0 END + CASE WHEN MOD(pb.pb188, 2) = 1 THEN COALESCE(dd.D752,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb189, 16)> 7 THEN COALESCE(dd.D753,  0) ELSE 0 END + CASE WHEN MOD(pb.pb189, 8) > 3 THEN COALESCE(dd.D754,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb189, 4) > 1 THEN COALESCE(dd.D755,  0) ELSE 0 END + CASE WHEN MOD(pb.pb189, 2) = 1 THEN COALESCE(dd.D756,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb190, 16)> 7 THEN COALESCE(dd.D757,  0) ELSE 0 END + CASE WHEN MOD(pb.pb190, 8) > 3 THEN COALESCE(dd.D758,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb190, 4) > 1 THEN COALESCE(dd.D759,  0) ELSE 0 END + CASE WHEN MOD(pb.pb190, 2) = 1 THEN COALESCE(dd.D760,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb191, 16)> 7 THEN COALESCE(dd.D761,  0) ELSE 0 END + CASE WHEN MOD(pb.pb191, 8) > 3 THEN COALESCE(dd.D762,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb191, 4) > 1 THEN COALESCE(dd.D763,  0) ELSE 0 END + CASE WHEN MOD(pb.pb191, 2) = 1 THEN COALESCE(dd.D764,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb192, 16)> 7 THEN COALESCE(dd.D765,  0) ELSE 0 END + CASE WHEN MOD(pb.pb192, 8) > 3 THEN COALESCE(dd.D766,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb192, 4) > 1 THEN COALESCE(dd.D767,  0) ELSE 0 END + CASE WHEN MOD(pb.pb192, 2) = 1 THEN COALESCE(dd.D768,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb193, 16)> 7 THEN COALESCE(dd.D769,  0) ELSE 0 END + CASE WHEN MOD(pb.pb193, 8) > 3 THEN COALESCE(dd.D770,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb193, 4) > 1 THEN COALESCE(dd.D771,  0) ELSE 0 END + CASE WHEN MOD(pb.pb193, 2) = 1 THEN COALESCE(dd.D772,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb194, 16)> 7 THEN COALESCE(dd.D773,  0) ELSE 0 END + CASE WHEN MOD(pb.pb194, 8) > 3 THEN COALESCE(dd.D774,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb194, 4) > 1 THEN COALESCE(dd.D775,  0) ELSE 0 END + CASE WHEN MOD(pb.pb194, 2) = 1 THEN COALESCE(dd.D776,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb195, 16)> 7 THEN COALESCE(dd.D777,  0) ELSE 0 END + CASE WHEN MOD(pb.pb195, 8) > 3 THEN COALESCE(dd.D778,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb195, 4) > 1 THEN COALESCE(dd.D779,  0) ELSE 0 END + CASE WHEN MOD(pb.pb195, 2) = 1 THEN COALESCE(dd.D780,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb196, 16)> 7 THEN COALESCE(dd.D781,  0) ELSE 0 END + CASE WHEN MOD(pb.pb196, 8) > 3 THEN COALESCE(dd.D782,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb196, 4) > 1 THEN COALESCE(dd.D783,  0) ELSE 0 END + CASE WHEN MOD(pb.pb196, 2) = 1 THEN COALESCE(dd.D784,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb197, 16)> 7 THEN COALESCE(dd.D785,  0) ELSE 0 END + CASE WHEN MOD(pb.pb197, 8) > 3 THEN COALESCE(dd.D786,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb197, 4) > 1 THEN COALESCE(dd.D787,  0) ELSE 0 END + CASE WHEN MOD(pb.pb197, 2) = 1 THEN COALESCE(dd.D788,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb198, 16)> 7 THEN COALESCE(dd.D789,  0) ELSE 0 END + CASE WHEN MOD(pb.pb198, 8) > 3 THEN COALESCE(dd.D790,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb198, 4) > 1 THEN COALESCE(dd.D791,  0) ELSE 0 END + CASE WHEN MOD(pb.pb198, 2) = 1 THEN COALESCE(dd.D792,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb199, 16)> 7 THEN COALESCE(dd.D793,  0) ELSE 0 END + CASE WHEN MOD(pb.pb199, 8) > 3 THEN COALESCE(dd.D794,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb199, 4) > 1 THEN COALESCE(dd.D795,  0) ELSE 0 END + CASE WHEN MOD(pb.pb199, 2) = 1 THEN COALESCE(dd.D796,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb200, 16)> 7 THEN COALESCE(dd.D797,  0) ELSE 0 END + CASE WHEN MOD(pb.pb200, 8) > 3 THEN COALESCE(dd.D798,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb200, 4) > 1 THEN COALESCE(dd.D799,  0) ELSE 0 END + CASE WHEN MOD(pb.pb200, 2) = 1 THEN COALESCE(dd.D800,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb201, 16)> 7 THEN COALESCE(dd.D801,  0) ELSE 0 END + CASE WHEN MOD(pb.pb201, 8) > 3 THEN COALESCE(dd.D802,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb201, 4) > 1 THEN COALESCE(dd.D803,  0) ELSE 0 END + CASE WHEN MOD(pb.pb201, 2) = 1 THEN COALESCE(dd.D804,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb202, 16)> 7 THEN COALESCE(dd.D805,  0) ELSE 0 END + CASE WHEN MOD(pb.pb202, 8) > 3 THEN COALESCE(dd.D806,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb202, 4) > 1 THEN COALESCE(dd.D807,  0) ELSE 0 END + CASE WHEN MOD(pb.pb202, 2) = 1 THEN COALESCE(dd.D808,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb203, 16)> 7 THEN COALESCE(dd.D809,  0) ELSE 0 END + CASE WHEN MOD(pb.pb203, 8) > 3 THEN COALESCE(dd.D810,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb203, 4) > 1 THEN COALESCE(dd.D811,  0) ELSE 0 END + CASE WHEN MOD(pb.pb203, 2) = 1 THEN COALESCE(dd.D812,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb204, 16)> 7 THEN COALESCE(dd.D813,  0) ELSE 0 END + CASE WHEN MOD(pb.pb204, 8) > 3 THEN COALESCE(dd.D814,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb204, 4) > 1 THEN COALESCE(dd.D815,  0) ELSE 0 END + CASE WHEN MOD(pb.pb204, 2) = 1 THEN COALESCE(dd.D816,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb205, 16)> 7 THEN COALESCE(dd.D817,  0) ELSE 0 END + CASE WHEN MOD(pb.pb205, 8) > 3 THEN COALESCE(dd.D818,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb205, 4) > 1 THEN COALESCE(dd.D819,  0) ELSE 0 END + CASE WHEN MOD(pb.pb205, 2) = 1 THEN COALESCE(dd.D820,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb206, 16)> 7 THEN COALESCE(dd.D821,  0) ELSE 0 END + CASE WHEN MOD(pb.pb206, 8) > 3 THEN COALESCE(dd.D822,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb206, 4) > 1 THEN COALESCE(dd.D823,  0) ELSE 0 END + CASE WHEN MOD(pb.pb206, 2) = 1 THEN COALESCE(dd.D824,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb207, 16)> 7 THEN COALESCE(dd.D825,  0) ELSE 0 END + CASE WHEN MOD(pb.pb207, 8) > 3 THEN COALESCE(dd.D826,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb207, 4) > 1 THEN COALESCE(dd.D827,  0) ELSE 0 END + CASE WHEN MOD(pb.pb207, 2) = 1 THEN COALESCE(dd.D828,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb208, 16)> 7 THEN COALESCE(dd.D829,  0) ELSE 0 END + CASE WHEN MOD(pb.pb208, 8) > 3 THEN COALESCE(dd.D830,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb208, 4) > 1 THEN COALESCE(dd.D831,  0) ELSE 0 END + CASE WHEN MOD(pb.pb208, 2) = 1 THEN COALESCE(dd.D832,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb209, 16)> 7 THEN COALESCE(dd.D833,  0) ELSE 0 END + CASE WHEN MOD(pb.pb209, 8) > 3 THEN COALESCE(dd.D834,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb209, 4) > 1 THEN COALESCE(dd.D835,  0) ELSE 0 END + CASE WHEN MOD(pb.pb209, 2) = 1 THEN COALESCE(dd.D836,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb210, 16)> 7 THEN COALESCE(dd.D837,  0) ELSE 0 END + CASE WHEN MOD(pb.pb210, 8) > 3 THEN COALESCE(dd.D838,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb210, 4) > 1 THEN COALESCE(dd.D839,  0) ELSE 0 END + CASE WHEN MOD(pb.pb210, 2) = 1 THEN COALESCE(dd.D840,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb211, 16)> 7 THEN COALESCE(dd.D841,  0) ELSE 0 END + CASE WHEN MOD(pb.pb211, 8) > 3 THEN COALESCE(dd.D842,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb211, 4) > 1 THEN COALESCE(dd.D843,  0) ELSE 0 END + CASE WHEN MOD(pb.pb211, 2) = 1 THEN COALESCE(dd.D844,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb212, 16)> 7 THEN COALESCE(dd.D845,  0) ELSE 0 END + CASE WHEN MOD(pb.pb212, 8) > 3 THEN COALESCE(dd.D846,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb212, 4) > 1 THEN COALESCE(dd.D847,  0) ELSE 0 END + CASE WHEN MOD(pb.pb212, 2) = 1 THEN COALESCE(dd.D848,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb213, 16)> 7 THEN COALESCE(dd.D849,  0) ELSE 0 END + CASE WHEN MOD(pb.pb213, 8) > 3 THEN COALESCE(dd.D850,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb213, 4) > 1 THEN COALESCE(dd.D851,  0) ELSE 0 END + CASE WHEN MOD(pb.pb213, 2) = 1 THEN COALESCE(dd.D852,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb214, 16)> 7 THEN COALESCE(dd.D853,  0) ELSE 0 END + CASE WHEN MOD(pb.pb214, 8) > 3 THEN COALESCE(dd.D854,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb214, 4) > 1 THEN COALESCE(dd.D855,  0) ELSE 0 END + CASE WHEN MOD(pb.pb214, 2) = 1 THEN COALESCE(dd.D856,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb215, 16)> 7 THEN COALESCE(dd.D857,  0) ELSE 0 END + CASE WHEN MOD(pb.pb215, 8) > 3 THEN COALESCE(dd.D858,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb215, 4) > 1 THEN COALESCE(dd.D859,  0) ELSE 0 END + CASE WHEN MOD(pb.pb215, 2) = 1 THEN COALESCE(dd.D860,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb216, 16)> 7 THEN COALESCE(dd.D861,  0) ELSE 0 END + CASE WHEN MOD(pb.pb216, 8) > 3 THEN COALESCE(dd.D862,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb216, 4) > 1 THEN COALESCE(dd.D863,  0) ELSE 0 END + CASE WHEN MOD(pb.pb216, 2) = 1 THEN COALESCE(dd.D864,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb217, 16)> 7 THEN COALESCE(dd.D865,  0) ELSE 0 END + CASE WHEN MOD(pb.pb217, 8) > 3 THEN COALESCE(dd.D866,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb217, 4) > 1 THEN COALESCE(dd.D867,  0) ELSE 0 END + CASE WHEN MOD(pb.pb217, 2) = 1 THEN COALESCE(dd.D868,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb218, 16)> 7 THEN COALESCE(dd.D869,  0) ELSE 0 END + CASE WHEN MOD(pb.pb218, 8) > 3 THEN COALESCE(dd.D870,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb218, 4) > 1 THEN COALESCE(dd.D871,  0) ELSE 0 END + CASE WHEN MOD(pb.pb218, 2) = 1 THEN COALESCE(dd.D872,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb219, 16)> 7 THEN COALESCE(dd.D873,  0) ELSE 0 END + CASE WHEN MOD(pb.pb219, 8) > 3 THEN COALESCE(dd.D874,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb219, 4) > 1 THEN COALESCE(dd.D875,  0) ELSE 0 END + CASE WHEN MOD(pb.pb219, 2) = 1 THEN COALESCE(dd.D876,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb220, 16)> 7 THEN COALESCE(dd.D877,  0) ELSE 0 END + CASE WHEN MOD(pb.pb220, 8) > 3 THEN COALESCE(dd.D878,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb220, 4) > 1 THEN COALESCE(dd.D879,  0) ELSE 0 END + CASE WHEN MOD(pb.pb220, 2) = 1 THEN COALESCE(dd.D880,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb221, 16)> 7 THEN COALESCE(dd.D881,  0) ELSE 0 END + CASE WHEN MOD(pb.pb221, 8) > 3 THEN COALESCE(dd.D882,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb221, 4) > 1 THEN COALESCE(dd.D883,  0) ELSE 0 END + CASE WHEN MOD(pb.pb221, 2) = 1 THEN COALESCE(dd.D884,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb222, 16)> 7 THEN COALESCE(dd.D885,  0) ELSE 0 END + CASE WHEN MOD(pb.pb222, 8) > 3 THEN COALESCE(dd.D886,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb222, 4) > 1 THEN COALESCE(dd.D887,  0) ELSE 0 END + CASE WHEN MOD(pb.pb222, 2) = 1 THEN COALESCE(dd.D888,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb223, 16)> 7 THEN COALESCE(dd.D889,  0) ELSE 0 END + CASE WHEN MOD(pb.pb223, 8) > 3 THEN COALESCE(dd.D890,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb223, 4) > 1 THEN COALESCE(dd.D891,  0) ELSE 0 END + CASE WHEN MOD(pb.pb223, 2) = 1 THEN COALESCE(dd.D892,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb224, 16)> 7 THEN COALESCE(dd.D893,  0) ELSE 0 END + CASE WHEN MOD(pb.pb224, 8) > 3 THEN COALESCE(dd.D894,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb224, 4) > 1 THEN COALESCE(dd.D895,  0) ELSE 0 END + CASE WHEN MOD(pb.pb224, 2) = 1 THEN COALESCE(dd.D896,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb225, 16)> 7 THEN COALESCE(dd.D897,  0) ELSE 0 END + CASE WHEN MOD(pb.pb225, 8) > 3 THEN COALESCE(dd.D898,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb225, 4) > 1 THEN COALESCE(dd.D899,  0) ELSE 0 END + CASE WHEN MOD(pb.pb225, 2) = 1 THEN COALESCE(dd.D900,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb226, 16)> 7 THEN COALESCE(dd.D901,  0) ELSE 0 END + CASE WHEN MOD(pb.pb226, 8) > 3 THEN COALESCE(dd.D902,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb226, 4) > 1 THEN COALESCE(dd.D903,  0) ELSE 0 END + CASE WHEN MOD(pb.pb226, 2) = 1 THEN COALESCE(dd.D904,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb227, 16)> 7 THEN COALESCE(dd.D905,  0) ELSE 0 END + CASE WHEN MOD(pb.pb227, 8) > 3 THEN COALESCE(dd.D906,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb227, 4) > 1 THEN COALESCE(dd.D907,  0) ELSE 0 END + CASE WHEN MOD(pb.pb227, 2) = 1 THEN COALESCE(dd.D908,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb228, 16)> 7 THEN COALESCE(dd.D909,  0) ELSE 0 END + CASE WHEN MOD(pb.pb228, 8) > 3 THEN COALESCE(dd.D910,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb228, 4) > 1 THEN COALESCE(dd.D911,  0) ELSE 0 END + CASE WHEN MOD(pb.pb228, 2) = 1 THEN COALESCE(dd.D912,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb229, 16)> 7 THEN COALESCE(dd.D913,  0) ELSE 0 END + CASE WHEN MOD(pb.pb229, 8) > 3 THEN COALESCE(dd.D914,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb229, 4) > 1 THEN COALESCE(dd.D915,  0) ELSE 0 END + CASE WHEN MOD(pb.pb229, 2) = 1 THEN COALESCE(dd.D916,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb230, 16)> 7 THEN COALESCE(dd.D917,  0) ELSE 0 END + CASE WHEN MOD(pb.pb230, 8) > 3 THEN COALESCE(dd.D918,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb230, 4) > 1 THEN COALESCE(dd.D919,  0) ELSE 0 END + CASE WHEN MOD(pb.pb230, 2) = 1 THEN COALESCE(dd.D920,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb231, 16)> 7 THEN COALESCE(dd.D921,  0) ELSE 0 END + CASE WHEN MOD(pb.pb231, 8) > 3 THEN COALESCE(dd.D922,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb231, 4) > 1 THEN COALESCE(dd.D923,  0) ELSE 0 END + CASE WHEN MOD(pb.pb231, 2) = 1 THEN COALESCE(dd.D924,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb232, 16)> 7 THEN COALESCE(dd.D925,  0) ELSE 0 END + CASE WHEN MOD(pb.pb232, 8) > 3 THEN COALESCE(dd.D926,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb232, 4) > 1 THEN COALESCE(dd.D927,  0) ELSE 0 END + CASE WHEN MOD(pb.pb232, 2) = 1 THEN COALESCE(dd.D928,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb233, 16)> 7 THEN COALESCE(dd.D929,  0) ELSE 0 END + CASE WHEN MOD(pb.pb233, 8) > 3 THEN COALESCE(dd.D930,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb233, 4) > 1 THEN COALESCE(dd.D931,  0) ELSE 0 END + CASE WHEN MOD(pb.pb233, 2) = 1 THEN COALESCE(dd.D932,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb234, 16)> 7 THEN COALESCE(dd.D933,  0) ELSE 0 END + CASE WHEN MOD(pb.pb234, 8) > 3 THEN COALESCE(dd.D934,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb234, 4) > 1 THEN COALESCE(dd.D935,  0) ELSE 0 END + CASE WHEN MOD(pb.pb234, 2) = 1 THEN COALESCE(dd.D936,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb235, 16)> 7 THEN COALESCE(dd.D937,  0) ELSE 0 END + CASE WHEN MOD(pb.pb235, 8) > 3 THEN COALESCE(dd.D938,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb235, 4) > 1 THEN COALESCE(dd.D939,  0) ELSE 0 END + CASE WHEN MOD(pb.pb235, 2) = 1 THEN COALESCE(dd.D940,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb236, 16)> 7 THEN COALESCE(dd.D941,  0) ELSE 0 END + CASE WHEN MOD(pb.pb236, 8) > 3 THEN COALESCE(dd.D942,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb236, 4) > 1 THEN COALESCE(dd.D943,  0) ELSE 0 END + CASE WHEN MOD(pb.pb236, 2) = 1 THEN COALESCE(dd.D944,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb237, 16)> 7 THEN COALESCE(dd.D945,  0) ELSE 0 END + CASE WHEN MOD(pb.pb237, 8) > 3 THEN COALESCE(dd.D946,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb237, 4) > 1 THEN COALESCE(dd.D947,  0) ELSE 0 END + CASE WHEN MOD(pb.pb237, 2) = 1 THEN COALESCE(dd.D948,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb238, 16)> 7 THEN COALESCE(dd.D949,  0) ELSE 0 END + CASE WHEN MOD(pb.pb238, 8) > 3 THEN COALESCE(dd.D950,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb238, 4) > 1 THEN COALESCE(dd.D951,  0) ELSE 0 END + CASE WHEN MOD(pb.pb238, 2) = 1 THEN COALESCE(dd.D952,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb239, 16)> 7 THEN COALESCE(dd.D953,  0) ELSE 0 END + CASE WHEN MOD(pb.pb239, 8) > 3 THEN COALESCE(dd.D954,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb239, 4) > 1 THEN COALESCE(dd.D955,  0) ELSE 0 END + CASE WHEN MOD(pb.pb239, 2) = 1 THEN COALESCE(dd.D956,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb240, 16)> 7 THEN COALESCE(dd.D957,  0) ELSE 0 END + CASE WHEN MOD(pb.pb240, 8) > 3 THEN COALESCE(dd.D958,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb240, 4) > 1 THEN COALESCE(dd.D959,  0) ELSE 0 END + CASE WHEN MOD(pb.pb240, 2) = 1 THEN COALESCE(dd.D960,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb241, 16)> 7 THEN COALESCE(dd.D961,  0) ELSE 0 END + CASE WHEN MOD(pb.pb241, 8) > 3 THEN COALESCE(dd.D962,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb241, 4) > 1 THEN COALESCE(dd.D963,  0) ELSE 0 END + CASE WHEN MOD(pb.pb241, 2) = 1 THEN COALESCE(dd.D964,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb242, 16)> 7 THEN COALESCE(dd.D965,  0) ELSE 0 END + CASE WHEN MOD(pb.pb242, 8) > 3 THEN COALESCE(dd.D966,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb242, 4) > 1 THEN COALESCE(dd.D967,  0) ELSE 0 END + CASE WHEN MOD(pb.pb242, 2) = 1 THEN COALESCE(dd.D968,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb243, 16)> 7 THEN COALESCE(dd.D969,  0) ELSE 0 END + CASE WHEN MOD(pb.pb243, 8) > 3 THEN COALESCE(dd.D970,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb243, 4) > 1 THEN COALESCE(dd.D971,  0) ELSE 0 END + CASE WHEN MOD(pb.pb243, 2) = 1 THEN COALESCE(dd.D972,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb244, 16)> 7 THEN COALESCE(dd.D973,  0) ELSE 0 END + CASE WHEN MOD(pb.pb244, 8) > 3 THEN COALESCE(dd.D974,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb244, 4) > 1 THEN COALESCE(dd.D975,  0) ELSE 0 END + CASE WHEN MOD(pb.pb244, 2) = 1 THEN COALESCE(dd.D976,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb245, 16)> 7 THEN COALESCE(dd.D977,  0) ELSE 0 END + CASE WHEN MOD(pb.pb245, 8) > 3 THEN COALESCE(dd.D978,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb245, 4) > 1 THEN COALESCE(dd.D979,  0) ELSE 0 END + CASE WHEN MOD(pb.pb245, 2) = 1 THEN COALESCE(dd.D980,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb246, 16)> 7 THEN COALESCE(dd.D981,  0) ELSE 0 END + CASE WHEN MOD(pb.pb246, 8) > 3 THEN COALESCE(dd.D982,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb246, 4) > 1 THEN COALESCE(dd.D983,  0) ELSE 0 END + CASE WHEN MOD(pb.pb246, 2) = 1 THEN COALESCE(dd.D984,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb247, 16)> 7 THEN COALESCE(dd.D985,  0) ELSE 0 END + CASE WHEN MOD(pb.pb247, 8) > 3 THEN COALESCE(dd.D986,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb247, 4) > 1 THEN COALESCE(dd.D987,  0) ELSE 0 END + CASE WHEN MOD(pb.pb247, 2) = 1 THEN COALESCE(dd.D988,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb248, 16)> 7 THEN COALESCE(dd.D989,  0) ELSE 0 END + CASE WHEN MOD(pb.pb248, 8) > 3 THEN COALESCE(dd.D990,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb248, 4) > 1 THEN COALESCE(dd.D991,  0) ELSE 0 END + CASE WHEN MOD(pb.pb248, 2) = 1 THEN COALESCE(dd.D992,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb249, 16)> 7 THEN COALESCE(dd.D993,  0) ELSE 0 END + CASE WHEN MOD(pb.pb249, 8) > 3 THEN COALESCE(dd.D994,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb249, 4) > 1 THEN COALESCE(dd.D995,  0) ELSE 0 END + CASE WHEN MOD(pb.pb249, 2) = 1 THEN COALESCE(dd.D996,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb250, 16)> 7 THEN COALESCE(dd.D997,  0) ELSE 0 END + CASE WHEN MOD(pb.pb250, 8) > 3 THEN COALESCE(dd.D998,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb250, 4) > 1 THEN COALESCE(dd.D999,  0) ELSE 0 END + CASE WHEN MOD(pb.pb250, 2) = 1 THEN COALESCE(dd.D1000,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb251, 16)> 7 THEN COALESCE(dd.D1001,  0) ELSE 0 END + CASE WHEN MOD(pb.pb251, 8) > 3 THEN COALESCE(dd.D1002,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb251, 4) > 1 THEN COALESCE(dd.D1003,  0) ELSE 0 END + CASE WHEN MOD(pb.pb251, 2) = 1 THEN COALESCE(dd.D1004,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb252, 16)> 7 THEN COALESCE(dd.D1005,  0) ELSE 0 END + CASE WHEN MOD(pb.pb252, 8) > 3 THEN COALESCE(dd.D1006,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb252, 4) > 1 THEN COALESCE(dd.D1007,  0) ELSE 0 END + CASE WHEN MOD(pb.pb252, 2) = 1 THEN COALESCE(dd.D1008,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb253, 16)> 7 THEN COALESCE(dd.D1009,  0) ELSE 0 END + CASE WHEN MOD(pb.pb253, 8) > 3 THEN COALESCE(dd.D1010,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb253, 4) > 1 THEN COALESCE(dd.D1011,  0) ELSE 0 END + CASE WHEN MOD(pb.pb253, 2) = 1 THEN COALESCE(dd.D1012,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb254, 16)> 7 THEN COALESCE(dd.D1013,  0) ELSE 0 END + CASE WHEN MOD(pb.pb254, 8) > 3 THEN COALESCE(dd.D1014,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb254, 4) > 1 THEN COALESCE(dd.D1015,  0) ELSE 0 END + CASE WHEN MOD(pb.pb254, 2) = 1 THEN COALESCE(dd.D1016,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb255, 16)> 7 THEN COALESCE(dd.D1017,  0) ELSE 0 END + CASE WHEN MOD(pb.pb255, 8) > 3 THEN COALESCE(dd.D1018,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb255, 4) > 1 THEN COALESCE(dd.D1019,  0) ELSE 0 END + CASE WHEN MOD(pb.pb255, 2) = 1 THEN COALESCE(dd.D1020,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb256, 16)> 7 THEN COALESCE(dd.D1021,  0) ELSE 0 END + CASE WHEN MOD(pb.pb256, 8) > 3 THEN COALESCE(dd.D1022,  0) ELSE 0 END +              
           CASE WHEN MOD(pb.pb256, 4) > 1 THEN COALESCE(dd.D1023,  0) ELSE 0 END + CASE WHEN MOD(pb.pb256, 2) = 1 THEN COALESCE(dd.D1024,  0) ELSE 0 END               
) AS yld_deduct FROM pb, dd)
SELECT yld_testdie,yld_deduct, PassBinList FROM yld_testdie,yld_deduct