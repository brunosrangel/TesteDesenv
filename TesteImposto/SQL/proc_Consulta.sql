CREATE PROCEDURE P_Lista_CPOF

AS


Select 
t2.Cfop
,t2.ValorIcms
,t2.BaseIcms
,t2.ValorIpi
,t2.BaseIpi 
from NotaFiscal t1
inner join NotaFiscalItem t2
on t1.NumeroNotaFiscal = t2.IdNotaFiscal

group by 
t2.Cfop
,t2.ValorIcms
,t2.BaseIcms
,t2.ValorIpi
,t2.BaseIpi 
   




