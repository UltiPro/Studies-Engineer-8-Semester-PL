DROP TABLE Sale;
DROP TABLE Zip;
DROP TABLE Time;
CREATE TABLE Zip (
    id NUMBER PRIMARY KEY,
    code VARCHAR2(5) NOT NULL,
    state VARCHAR2(2) NOT NULL
);
CREATE TABLE Time (
    id NUMBER(3) PRIMARY KEY,
    MONTH NUMBER(2),
    YEAR NUMBER(4)
);
CREATE TABLE Sale (
    id NUMBER PRIMARY KEY,
    id_time NUMBER(3) REFERENCES Time(id) ON DELETE CASCADE,
    id_zip NUMBER REFERENCES Zip(id) ON DELETE CASCADE
);
DROP SEQUENCE S_TIME;     
DROP SEQUENCE S_ZIP;    
DROP SEQUENCE S_SALE;   
CREATE SEQUENCE S_TIME;
CREATE SEQUENCE S_ZIP;
CREATE SEQUENCE S_SALE;
CREATE OR REPLACE TRIGGER T_SET_ID_TIME
BEFORE INSERT 
ON Time
FOR EACH ROW
BEGIN
    :NEW.id := S_TIME.NEXTVAL;
END;
/
CREATE OR REPLACE TRIGGER T_SET_ID_ZIP
BEFORE INSERT 
ON Zip
FOR EACH ROW
BEGIN
    :NEW.id := S_ZIP.NEXTVAL;
END;
/
CREATE OR REPLACE TRIGGER T_SET_ID_SALE
BEFORE INSERT 
ON Sale
FOR EACH ROW
BEGIN
    :NEW.id := S_SALE.NEXTVAL;
END;
/
CREATE OR REPLACE FUNCTION F_PROFIT_BY_STATE(P_STATE ORDERS.SHIPSTATE%TYPE) RETURN NUMBER IS
PROFIT NUMBER;
BEGIN
    SELECT SUM(OI.QUANTITY*(B.RETAIL-B.COST))
    INTO PROFIT
    FROM BOOKS B Join ORDERITEMS OI ON B.ISBN = OI.ISBN JOIN ORDERS O ON OI.ORDER# = O.ORDER#
    WHERE UPPER(O.SHIPSTATE)=UPPER(P_STATE);
RETURN PROFIT;
END F_PROFIT_BY_STATE;