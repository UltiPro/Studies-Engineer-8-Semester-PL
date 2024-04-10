CREATE OR REPLACE VIEW V_CUSTOMERS AS SELECT * FROM CUSTOMERS;

CREATE OR REPLACE VIEW V_AUTHOR AS SELECT * FROM AUTHOR;

CREATE OR REPLACE VIEW V_BOOKS AS SELECT * FROM BOOKS;

CREATE OR REPLACE VIEW V_PUBLISHER AS SELECT * FROM PUBLISHER;

CREATE OR REPLACE VIEW V_TIME AS SELECT * FROM D_TIME;

CREATE OR REPLACE VIEW V_SALE AS 

                SELECT P.PUBID, B.ISBN, C.CUSTOMER#, T.ID_TIME,

                        A.AUTHORID,

                        SUM(OI.QUANTITY) QUANTITY,

                        SUM(OI.QUANTITY*(B.RETAIL-B.COST)) PROFIT,

                        SUM(OI.QUANTITY*B.RETAIL) SALE_AMOUNT

                FROM PUBLISHER P

                        JOIN BOOKS B ON P.PUBID=B.PUBID

                        JOIN ORDERITEMS OI ON B.ISBN=OI.ISBN

                        JOIN ORDERS O ON OI.ORDER#=O.ORDER#

                        JOIN CUSTOMERS C ON C.CUSTOMER#=O.CUSTOMER#

                        JOIN BOOKAUTHOR BA ON BA.ISBN=B.ISBN

                        JOIN AUTHOR A ON A.AUTHORID=BA.AUTHORID

                        JOIN D_TIME T ON T.ORDERDATE=O.ORDERDATE

                GROUP BY P.PUBID, B.ISBN, C.CUSTOMER#, T.ID_TIME,

                        A.AUTHORID;

                        

----- ZADANIE 2-----------------------------

SELECT C.FIRSTNAME||' '||C.LASTNAME CUSTOMER,

        T.YEAR, T.MONTH_NUMBER, SUM(S.QUANTITY) QUANTITY,

        SUM(S.SALE_AMOUNT) SALE_AMOUNT

FROM V_CUSTOMERS C JOIN V_SALE S ON C.CUSTOMER#=S.CUSTOMER#

        JOIN V_TIME T ON T.ID_TIME=S.ID_TIME

GROUP BY GROUPING SETS

     (

     (C.CUSTOMER#, C.FIRSTNAME, C.LASTNAME, T.YEAR, T.MONTH_NUMBER),

     ()

     );

----- ZADANIE 4-----------------------------

        

SELECT  T.YEAR, A.FNAME||' '||A.LNAME AUTHOR,

        SUM(S.QUANTITY) QUANTITY,

        SUM(S.SALE_AMOUNT) SALE_AMOUNT

FROM V_AUTHOR A JOIN V_SALE S ON A.AUTHORID=S.AUTHORID

        JOIN V_TIME T ON T.ID_TIME=S.ID_TIME

GROUP BY ROLLUP(T.YEAR, (A.AUTHORID, A.FNAME, A.LNAME));

----- ZADANIE 5-----------------------------

SELECT T.DAY_NAME, 

        DECODE(C.CITY,NULL, ' ',C.CITY) CITY,

        DECODE(C.STATE, NULL,' ',C.STATE) STATE,

        SUM(S.QUANTITY) QUANTITY,

        GROUPING(T.DAY_NAME) G_D,

        GROUPING(C.CITY) G_C,

        GROUPING(C.STATE) G_S,

        GROUPING_ID(T.DAY_NAME,C.CITY,C.STATE) G_ID

FROM V_CUSTOMERS C JOIN V_SALE S ON C.CUSTOMER#=S.CUSTOMER#

        JOIN V_TIME T ON T.ID_TIME=S.ID_TIME

GROUP BY CUBE(T.DAY_NAME, C.CITY, C.STATE)

--HAVING GROUPING_ID(T.DAY_NAME,C.CITY,C.STATE) IN (1,3,6)

ORDER BY G_ID;

----- ZADANIE 7-----------------------------

CREATE TABLE KOSTKA AS 

SELECT T.YEAR, T.MONTH_NUMBER, C.CITY, B.CATEGORY,

        SUM(S.PROFIT) PROFIT

FROM V_CUSTOMERS C JOIN V_SALE S ON C.CUSTOMER#=S.CUSTOMER#

        JOIN V_TIME T ON T.ID_TIME=S.ID_TIME 

        JOIN V_BOOKS B ON B.ISBN=S.ISBN

GROUP BY ROLLUP((T.YEAR, T.MONTH_NUMBER), C.CITY, B.CATEGORY);

-----------------ZADANIE DODATKOWE-------------------

SELECT *

FROM 

(

    SELECT DECODE(T.YEAR,NULL,'SUMMARY',T.YEAR) YEAR,

            B.CATEGORY,

            SUM(S.PROFIT) PROFIT

    FROM V_SALE S JOIN V_TIME T ON T.ID_TIME=S.ID_TIME 

            JOIN V_BOOKS B ON B.ISBN=S.ISBN

    GROUP BY CUBE(T.YEAR, B.CATEGORY)

) PIVOT (SUM(PROFIT) FOR YEAR IN 

        (2005,2006,2008,2009,2010,'SUMMARY') );