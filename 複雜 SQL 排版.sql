--瑞鼎出貨記錄
SELECT ((SELECT cust FROM pdt p WHERE p.r_id = main.r_id) || '股份有限公司' )AS "客戶",
       CASE WHEN mm.prober IS NULL             
        THEN (SELECT DISTINCT machine1
                FROM machine_status_log
               WHERE rcno = main.rc_root
                 AND (status ='SETUP' OR status ='WORK')
            ORDER BY machine1 desc limit 1)                
        ELSE mm.prober  
   END "Prober",
   CASE WHEN mm.tester IS NULL
        THEN (SELECT DISTINCT machine2
        FROM machine_status_log
       WHERE rcno = main.rc_root
         AND (status ='SETUP' OR status ='WORK')
        ORDER BY machine2 desc limit 1)                       
            ELSE mm.tester          
       END "Tester",
 main.cst_sc_date AS "進貨日期",
 main.cst_sc AS "委工單號",
 CASE WHEN COALESCE(main.device_code, '') != '' 
      THEN CONCAT(main.device_body, '-', COALESCE(main.device_code, ''))
      ELSE main.device_body
      END "型號",
 main.finished_item AS "完成型號",
 main.lotid AS "批號",
 vf.belong AS "製程",       
 main.co_qty AS "進貨數(PCS)",
 main.pcs AS "出貨數(PCS)",
 '' AS "單價",
 '' AS "金額",
 '' AS "幣別",
 (SELECT SUM(mm.testdie))         
 AS "GrossDie(EA)",
 (SELECT SUM(mm.passdie))
 AS "GoodDie(EA)",
 ROUND((( CAST(SUM(mm.passdie) AS NUMERIC) / CAST(SUM(mm.testdie)AS NUMERIC))*100),3) AS "Yield",
 main.now_station AS "Station",
 main.pstatus AS "Status",
 CASE WHEN main.note = '' THEN to_char(current_timestamp, 'YYYY/MM/DD')
      WHEN main.note IS NULL THEN to_char(current_timestamp, 'YYYY/MM/DD')
      ELSE SUBSTRING(main.note,1,10)
 END "結案日期",   /* 如果沒有設定結案日期，則先帶當下的時間 */
 vs.dut AS "Dut",
 CASE WHEN (SELECT SUBSTRING(vs.pgm fROM 1 FOR 2) = 'P:')
      THEN RTRIM(CAST(SPLIT_PART(REPLACE(vs.pgm, '/', '\'),
                    '\',
                    LENGTH(vs.pgm) - LENGTH(REPLACE(REPLACE(vs.pgm, '/', '\'), '\', '')) + 1
                         ) AS VARCHAR), '.EXE')
       ELSE vs.pgm
  END "測試程式",
  vs.t_time AS "秒數",
  main.r_id AS "R/C No.",
  (SELECT NULLIF(REPLACE(REPLACE(REGEXP_REPLACE(CAST(d.wafer_id_set AS VARCHAR ), '[{},\"\"]', '', 'g'), ';', ',') || 'E', ',E', ''), 'E')    
            FROM (SELECT ARRAY(SELECT CASE WHEN SUBSTRING('0' || vw.wfid || '0', generate_series + 1, 2) = '10'  
                                       THEN CAST(generate_series AS VARCHAR) || ';'    
              WHEN SUBSTRING('0' || vw.wfid || '0', generate_series, 3) = '011'  
              THEN CAST(generate_series AS VARCHAR) || '-'   
                   ELSE ''                       
              END                                 
         FROM generate_series(1, LENGTH((SELECT wfid FROM v_wfid v WHERE v.r_id = main.r_id))) 
                   ) AS wafer_id_set     
      ) d              
             ) AS "刻號"
FROM ( SELECT p.r_id, p.rcard, p.lotid, p.pcs, p.rc_root, co.cst_sc_date, co.cst_sc,
          co.finished_item, co.device_body, co.device_code, co.co_qty,
          w.now_station, w.pstatus, wi.note             
     FROM pdt p, wip w, customer_order co, w_inv wi                
    WHERE p.r_id = w.r_id
    AND p.co = co.co
    AND p.r_id = wi.r_id             
    AND p.cust='瑞鼎科技'
    AND w.pstatus = '已結案'
      AND w.now_station = '庫房'        
  )AS main
  LEFT JOIN view_flow vf ON (vf.rcard = main.rcard)
  LEFT JOIN wiplog w2 ON (w2.r_id = main.r_id AND w2.step = vf.step)
  LEFT JOIN v_wfid vw ON ( vw.r_id = main.r_id )
  LEFT JOIN map_mst mm ON (mm.step = vf.belong AND mm.rcno = main.rc_root AND SUBSTRING(vw.wfid, mm.wfid, 1) = '1')  
  LEFT JOIN view_sinfo vs ON (vs.seqno = CAST (mm.specno AS INT) AND vs.step = vf.belong)  
WHERE w2.bypass='N'  
   AND vf.belong LIKE '%CP%'
GROUP BY main.r_id,mm.prober,mm.tester,main.cst_sc_date,main.cst_sc,main.device_code,main.device_body,main.finished_item,
         main.lotid,vf.belong, vf.belong,main.co_qty,main.pcs,main.rc_root,vw.wfid,main.now_station,main.pstatus,main.note,
         vs.dut,vs.pgm,vs.t_time 
ORDER BY main.r_id desc,vf.belong


--UMC出貨單測試
SELECT ws.out_date,
       ws.outno,
       ws.item,
    ws.r_id,
    ws.dvcname_m,
    ws.dvcname_s,
    p.lotid,
    p.cst_no,
    ws.outwfid,
    ws.outpcs,
    ws.outpass,
       ws.outfail,
       se.step,
  (SELECT lvd2.mapping_value
   FROM list_value_master lvm,
      list_value_detail lvd,
      list_value_detail lvd2
 WHERE lvm.type_id = 'SHIP_TO_W'
   AND lvm.type_id = lvd.type_id
   AND lvd.code_id = p.cst_no
   AND lvd.mapping_value = lvd2.type_id
     AND lvd2.code_id = se.ship_to ) AS ship_subcon_code,
       StrPos(ws.outwfid, '1') AS MinWfId
FROM w_ship ws,
     ship_ext se,
     pdt p
WHERE ws.outno = '20180515'
  AND ws.r_id = p.r_id
  AND ws.outno = se.outno
  AND ws.item = se.item
UNION
SELECT ws.out_date,
  ws.outno,
  ws.item,
  ws.r_id,
  ws.dvcname_m,
  ws.dvcname_s,
  p.lotid,
  p.cst_no,
  ws.outwfid,
  ws.outpcs,
  ws.outpass,
       ws.outfail,
       COALESCE(
   (SELECT s.step
    FROM w_ship w, ship_ext s
    WHERE p.r_id = w.r_id
      AND w.outno = s.outno
      AND w.item = s.item
      AND s.invf = 'Y'
      AND STRPOS(CAST(CAST(w.outwfid AS NUMERIC)+CAST(ws.outwfid AS NUMERIC) AS VARCHAR), '2')>0
    LIMIT 1), se.step) AS step,
  (SELECT lvd2.mapping_value
 FROM list_value_master lvm,
      list_value_detail lvd,
      list_value_detail lvd2
 WHERE lvm.type_id = 'SHIP_TO_W'
   AND lvm.type_id = lvd.type_id
   AND lvd.code_id = p.cst_no
   AND lvd.mapping_value = lvd2.type_id
   AND lvd2.code_id = se.ship_to ) AS ship_subcon_code,
     StrPos(ws.outwfid, '1') AS MinWfId
FROM w_ship_fg ws,
 ship_ext_fg se,
   pdt p
WHERE ws.outno = '20180515'
 AND ws.r_id = p.r_id
AND ws.outno = se.outno
 AND ws.item = se.item
ORDER BY outno, item