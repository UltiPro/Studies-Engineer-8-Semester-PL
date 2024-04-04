DROP TABLE F_Sprzedaz
/
DROP TABLE W_Date
/
DROP TABLE W_Product
/
DROP TABLE W_Customer
/
CREATE TABLE W_Customer (
 CUSTOMER_ID NUMBER(5) PRIMARY KEY,
 EMAILID VARCHAR2(128 BYTE)
)
/
CREATE TABLE W_Product (
 PRODUCT_ID NUMBER(5) PRIMARY KEY,
 PRODUCT_NAME VARCHAR2(128 BYTE)
)
/
CREATE TABLE W_Date (
 DATE_ID NUMBER(5) PRIMARY KEY,
 DATA DATE,
 NR_DNIA_TYG NUMBER(1),
 CZY_WEEKEND NUMBER(1,0),
 NAZWA_DNIA_TYG VARCHAR2(127 BYTE),
 NAZWA_MIESIACA VARCHAR2(127 BYTE),
 NUMER_MIESIACA NUMBER(2),
 ROK NUMBER(4)
)
/
CREATE TABLE F_Sprzedaz (
 DATE_ID REFERENCES W_Date(DATE_ID),
 PRODUCT_ID REFERENCES W_Product(PRODUCT_ID),
 CUSTOMER_ID REFERENCES W_Customer(CUSTOMER_ID),
 Wartosc FLOAT(2),
 Zysk FLOAT(2)
)
/
DROP SEQUENCE W_Date_SEQ
/
CREATE SEQUENCE W_Date_SEQ
START WITH 1
INCREMENT BY 1
/
CREATE OR REPLACE TRIGGER T_W_Date_SEQ
BEFORE INSERT
ON W_Date
FOR EACH ROW
BEGIN
    :NEW.DATE_ID := W_Date_SEQ.NEXTVAL;
END;
/
MERGE INTO W_Customer WC
USING(SELECT CUSTOMER_ID, EMAILID FROM CUSTOMER_TEMP) SC
ON (WC.CUSTOMER_ID = SC.CUSTOMER_ID)
WHEN NOT MATCHED THEN
INSERT (CUSTOMER_ID, EMAILID) VALUES(SC.CUSTOMER_ID, SC.EMAILID)
/
MERGE INTO W_Product WP
USING(SELECT PRODUCT_ID, PRODUCT_NAME FROM PRODUCT_TEMP) SP
ON (WP.PRODUCT_ID = SP.PRODUCT_ID)
WHEN NOT MATCHED THEN
INSERT (PRODUCT_ID, PRODUCT_NAME) VALUES(SP.PRODUCT_ID, SP.PRODUCT_NAME)
/
CREATE OR REPLACE FUNCTION F_CZY_WEEKEND(Wej DATE)
RETURN NUMBER
IS RESULT NUMBER(1,0);
BEGIN
    SELECT (CASE WHEN TO_CHAR(Wej, 'DY', 'NLS_DATE_LANGUAGE=ENGLISH') IN ('SAT', 'SUN') THEN 1 ELSE 0 END) INTO RESULT
    FROM DUAL;
RETURN RESULT;
END;
/
MERGE INTO W_Date WD
USING(SELECT DISTINCT ORDER_DATE AS OD, 
                      TO_NUMBER(TO_CHAR(ORDER_DATE, 'D')) AS NR_DNIA_TYG,
                      F_CZY_WEEKEND(ORDER_DATE) AS CZY_WEEKEND,
                      TO_CHAR(ORDER_DATE, 'DAY') AS NAZWA_DNIA_TYG,
                      TO_CHAR(ORDER_DATE, 'MONTH') AS NAZWA_MIESIACA,
                      EXTRACT(MONTH FROM ORDER_DATE) AS NUMER_MIESIACA,
                      EXTRACT(YEAR FROM ORDER_DATE) AS ROK
      FROM ORDER_TEMP) SOD
ON(WD.DATA = SOD.OD)
WHEN NOT MATCHED THEN
INSERT (DATA, NR_DNIA_TYG, CZY_WEEKEND, NAZWA_DNIA_TYG, NAZWA_MIESIACA, NUMER_MIESIACA, ROK)
VALUES (SOD.OD, SOD.NR_DNIA_TYG, SOD.CZY_WEEKEND, SOD.NAZWA_DNIA_TYG, SOD.NAZWA_MIESIACA, SOD.NUMER_MIESIACA, SOD.ROK);
/
MERGE INTO F_Sprzedaz FS
USING (SELECT WD.DATE_ID AS DI, OPT.PRODUCT_ID AS PI, OT.CUSTOMER_ID AS CI, SUM(OPT.SALES) AS Wartosc, SUM(OPT.PROFIT) AS Zysk
       FROM W_Date WD, ORDER_PRODUCT_TEMP OPT, ORDER_TEMP OT
       WHERE OT.ORDER_ID = OPT.ORDER_ID AND WD.DATA = OT.ORDER_DATE
       GROUP BY WD.DATE_ID, OPT.PRODUCT_ID, OT.CUSTOMER_ID) SDS
ON (FS.DATE_ID = SDS.DI AND FS.PRODUCT_ID = SDS.PI AND FS.CUSTOMER_ID = SDS.CI)
WHEN NOT MATCHED THEN
INSERT (DATE_ID, PRODUCT_ID, CUSTOMER_ID, Wartosc, Zysk)
VALUES (SDS.DI, SDS.PI, SDS.CI, SDS.Wartosc, SDS.Zysk);