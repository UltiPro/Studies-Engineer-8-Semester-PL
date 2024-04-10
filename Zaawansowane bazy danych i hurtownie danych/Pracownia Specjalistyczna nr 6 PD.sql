// ZAD 1

SELECT C.Country VISITED_COUNTRY, SUM(V.number_of_visits) NUM, RANK() OVER(ORDER BY SUM(V.number_of_visits) DESC) RANKING
FROM City C, Visit V, Visitorcountry VC
WHERE C.id_city = V.id_city AND V.id_country = VC.id_country
GROUP BY C.Country;

SELECT VC.visitor_country VISITOR_COUNTRY, SUM(V.number_of_visitors) NUM, RANK() OVER(ORDER BY SUM(V.number_of_visitors) DESC) RANKING
FROM City C, Visit V, Visitorcountry VC
WHERE C.id_city = V.id_city AND V.id_country = VC.id_country
GROUP BY VC.visitor_country;

SELECT T1.VISITED_COUNTRY, T2.VISITOR_COUNTRY, T1.RANKING
FROM 
    (SELECT C.Country VISITED_COUNTRY, SUM(V.number_of_visits) NUM, RANK() OVER(ORDER BY SUM(V.number_of_visits) DESC) RANKING
    FROM City C, Visit V, Visitorcountry VC
    WHERE C.id_city = V.id_city AND V.id_country = VC.id_country
    GROUP BY C.Country) T1
    JOIN
    (SELECT VC.visitor_country VISITOR_COUNTRY, SUM(V.number_of_visitors) NUM, DENSE_RANK() OVER(ORDER BY SUM(V.number_of_visitors) DESC) RANKING
    FROM City C, Visit V, Visitorcountry VC
    WHERE C.id_city = V.id_city AND V.id_country = VC.id_country
    GROUP BY VC.visitor_country) T2
    ON T1.RANKING = T2.RANKING;


// ZAD 2

/*SELECT VT.description, T.year, T.month, SUM(V.profit) PROFIT
FROM VisitType VT, Time T, Visit V
WHERE V.id_visit_type = VT.id_visit_type AND T.id_time = V.id_time
GROUP BY VT.description, T.year, T.month
ORDER BY 1, 4  DESC;*/

SELECT VT.description, DECODE(T.year, NULL, 'SUMMARY:', T.year) YEAR, DECODE(T.month, NULL, '----', T.month) MONTH, SUM(V.profit) PROFIT, 
    DECODE(T.YEAR, NULL, '----', ROUND(AVG(SUM(V.profit)) OVER(PARTITION BY VT.description ORDER BY VT.description, T.year, T.month NULLS LAST ROWS BETWEEN 1 PRECEDING AND 2 FOLLOWING), 2)) AVERAGE
FROM VisitType VT, Time T, Visit V
WHERE V.id_visit_type = VT.id_visit_type AND T.id_time = V.id_time
GROUP BY GROUPING SETS((VT.description, T.year, T.month), VT.description);

// ZAD 3

/*SELECT DECODE(C.continent, NULL, 'TOTAL:', C.continent) Continent, 
        DECODE(VT.description, NULL, 'TOTAL', VT.description) VISIT_TYPE, 
        SUM(V.number_of_visitors) NUM_OF_VISITORS, 
        ROUND(AVG(V.number_of_visitors), 2) AVG_OF_VISITORS
    FROM City C, Visit V, VisitType VT
    WHERE C.id_city = V.id_city AND V.id_visit_type = VT.id_visit_type
    GROUP BY CUBE(C.continent, VT.description);*/


SELECT *
FROM (
    SELECT DECODE(C.continent, NULL, 'TOTAL:', C.continent) Continent, 
        DECODE(VT.description, NULL, 'TOTAL', VT.description) VISIT_TYPE, 
        SUM(V.number_of_visitors) NUM_OF_VISITORS, 
        ROUND(AVG(V.number_of_visitors), 2) AVG_OF_VISITORS
    FROM City C, Visit V, VisitType VT
    WHERE C.id_city = V.id_city AND V.id_visit_type = VT.id_visit_type
    GROUP BY CUBE(C.continent, VT.description))
PIVOT (
    SUM(NUM_OF_VISITORS) SUM,
    AVG(AVG_OF_VISITORS) AVG
    FOR VISIT_TYPE IN ('Business', 'Individual', 'Organized group', 'TOTAL')
)
ORDER BY 1;

// ZAD 4

/*SELECT T.year, AT.name, SUM(V.profit) PROFIT
FROM Time T, Accommodationtype AT, Visit V
WHERE T.id_time = V.id_time AND V.id_type = AT.id_type
GROUP BY T.year, AT.name
ORDER BY 1;*/

/*SELECT T.year, AT.name, SUM(V.profit) PROFIT, NTILE(4) OVER(PARTITION BY T.year ORDER BY SUM(V.profit) DESC) NTILE
FROM Time T, Accommodationtype AT, Visit V
WHERE T.id_time = V.id_time AND V.id_type = AT.id_type
GROUP BY T.year, AT.name
ORDER BY T.YEAR, PROFIT DESC;*/

SELECT YEAR, SUM(PROFIT) FIRST_QUARTER_PROFIT, NTILE, ROUND(SUM(PROFIT) / SUM(SUM(PROFIT)) OVER () * 100, 1) PERCENTAGE
FROM (SELECT T.year, AT.name, SUM(V.profit) PROFIT, NTILE(4) OVER(PARTITION BY T.year ORDER BY SUM(V.profit) DESC) NTILE
    FROM Time T, Accommodationtype AT, Visit V
    WHERE T.id_time = V.id_time AND V.id_type = AT.id_type
    GROUP BY T.year, AT.name
    ORDER BY T.YEAR, PROFIT DESC)
WHERE NTILE = 1
GROUP BY YEAR, NTILE;

// ZAD 5

SELECT VC.visitor_country VISITOR_COUNTRY, C.country COUNTRY_TO_VISIT, SUM(V.number_of_visitors) VISITORS
FROM VisitorCountry VC JOIN Visit V ON VC.id_country = V.id_country JOIN City C ON V.id_city = C.id_city
GROUP BY VC.visitor_country, C.country
ORDER BY 1;

SELECT VC.visitor_country, DECODE(SUM(V.number_of_visitors), NULL, '--NO DATA--', SUM(V.number_of_visitors)) NUM_COUNTRY_VISITORS
FROM VisitorCountry VC LEFT JOIN Visit V ON VC.id_country = V.id_country
GROUP BY VC.visitor_country;

SELECT T2.from_country VISITOR_COUNTRY, 
    DECODE(T1.COUNTRY_TO_VISIT, NULL, '--NO DATA--', T1.COUNTRY_TO_VISIT) COUNTRY_TO_VISIT, 
    DECODE(T1.Visitors, NULL, 0 , T1.Visitors) VISITORS, 
    DECODE(ROUND(T1.Visitors / T2.num_country_visitors, 2), NULL, 0, ROUND(T1.Visitors / T2.num_country_visitors, 3)) * 100 PERCENTAGE
FROM
    (SELECT VC.visitor_country VISITOR_COUNTRY, C.country COUNTRY_TO_VISIT, SUM(V.number_of_visitors) VISITORS
     FROM VisitorCountry VC JOIN Visit V ON VC.id_country = V.id_country JOIN City C ON V.id_city = C.id_city
     GROUP BY VC.visitor_country, C.country
     ORDER BY 1) T1 RIGHT JOIN 
    (SELECT VC.visitor_country FROM_COUNTRY, DECODE(SUM(V.number_of_visitors), NULL, '--NO DATA--', SUM(V.number_of_visitors)) NUM_COUNTRY_VISITORS
    FROM VisitorCountry VC LEFT JOIN Visit V ON VC.id_country = V.id_country
    GROUP BY VC.visitor_country) T2 ON T1.visitor_country = T2.from_country
ORDER BY 1, 2;