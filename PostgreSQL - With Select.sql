with t1 as (
select tt.dvcname_m as "DeviceName", pp.rcno as "Runcard", tt.lotid as "Lot No", pp.specno, gg.in_time as "CheckInTime", 
sum(pp.testdie) as "TestQty", sum(pp.passdie) as "PassQty", sum(pp.faildie) as "FailQty",
sum(pp.b01) as "Bin1", sum(pp.b02) as "Bin2", sum(pp.b03) as "Bin3", sum(pp.b04) as "Bin4", sum(pp.b05) as "Bin5", sum(pp.b06) as "Bin6", sum(pp.b07) as "Bin7", sum(pp.b08) as "Bin8", sum(pp.b09) as "Bin9", sum(pp.b10) as "Bin10",
'0' as "Scrap Qty", '0' as "Loss Qty", '0' as "Other Qty",
pp.step as "Step", round(sum(yield)/count(*),2) as "Final Yield"
 from view_map pp, pdt tt, wiplog gg, view_sinfo oo where pp.rcno=tt.r_id and tt.r_id=gg.r_id and pp.step=gg.step
and pp.rcno='A8043040' group by pp.rcno, pp.step, tt.dvcname_m, tt.lotid, gg.in_time, pp.specno
order by pp.rcno, pp.step
),
t2 as (
select ROW_NUMBER() OVER (PARTITION BY "Runcard" ORDER BY "CheckInTime" desc) AS seq,
"DeviceName", "Runcard", "Lot No", "Step",
 (select "Final Yield" from t1 t where t1."Runcard" = t."Runcard" and "Step" = 'CP1') AS cp1_final_yield,
 (select "Final Yield" from t1 t where t1."Runcard" = t."Runcard" and "Step" = 'CP2') AS cp2_final_yield,
 (select "Final Yield" from t1 t where t1."Runcard" = t."Runcard" and "Step" = 'CP3') AS cp3_final_yield
from t1
)
select * 
from t2
where seq = 1
