--ZAD 1--

SELECT DECODE(A.AGE_FROM,NULL,0,A.AGE_FROM)||'-'|| DECODE(A.AGE_TO,NULL,'BRAK',A.AGE_TO) GRUPA_WIEKOWA, 
    T.year, 
    SUM(V.number_of_visitors) LICZBA_ODW, 
    ROUND(AVG(SUM(V.number_of_visitors)) OVER(PARTITION BY DECODE(A.AGE_FROM,NULL,0,A.AGE_FROM)||'-'|| DECODE(A.AGE_TO,NULL,'BRAK',A.AGE_TO)), 2) AVG
FROM Age A, Visit V, Time T
WHERE A.id_age = V.id_age AND T.id_time = V.id_time
GROUP BY A.age_from, A.age_to, T.year
ORDER BY A.age_from, A.age_to;

--ZAD 2--

SELECT AT.name, VC.visitor_continent, SUM(V.number_of_visitors) numofvistors
FROM AccommodationType AT, VisitorCountry VC, Visit V
WHERE AT.id_type = V.id_type AND V.id_country = VC.id_country
GROUP BY CUBE(AT.name, VC.visitor_continent);

SELECT *
FROM (SELECT DECODE(AT.name, NULL, 'SUMMARY', AT.name), DECODE(TRIM(C.CONTINENT), NULL, 'TOTAL',C.CONTINENT) CONTINENT, SUM(V.number_of_visitors) numofvistors
    FROM AccommodationType AT, City C, Visit V
    WHERE AT.id_type = V.id_type AND V.id_city = C.id_city
    GROUP BY CUBE(AT.name, C.continent))
    PIVOT (
        SUM(numofvistors) FOR CONTINENT IN ('Europe','South America','TOTAL')
    )
    ORDER BY 1;
    
--ZAD 3--

SELECT T.year, C.name, SUM(V.number_of_visitors) LV, FIRST_VALUE(SUM(V.number_of_visitors)) OVER(PARTITION BY T.year) MAX_V, LAST_VALUE(SUM(V.number_of_visitors)) OVER(PARTITION BY T.year) MIN_V
FROM City C, Visit V, Time T
WHERE T.id_time = V.id_time AND V.id_city = C.id_city
GROUP BY T.year, C.name
ORDER BY T.year, SUM(V.number_of_visitors) DESC;

--ZAD 4--

SELECT VC.visitor_country, RANK() OVER(ORDER BY Sum(V.number_of_visitors) DESC) Visitors_rank, RANK() OVER(ORDER BY SUM(V.number_of_visits) ASC) Visits_rank
    FROM VisitorCountry VC, Visit V
    WHERE V.id_country = VC.id_country
    GROUP BY VC.visitor_country

SELECT *
FROM (SELECT VC.visitor_country, RANK() OVER(ORDER BY Sum(V.number_of_visitors) DESC) Visitors_rank, RANK() OVER(ORDER BY SUM(V.number_of_visits) ASC) Visits_rank
    FROM VisitorCountry VC, Visit V
    WHERE V.id_country = VC.id_country
    GROUP BY VC.visitor_country)
WHERE Visitors_rank IN (1,2,3) OR Visits_rank IN (1,2,3)

--ZAD 5--

SELECT VC.visitor_continent, T.year, AT.name, SUM(V.number_of_visitors) Visitors, SUM(V.profit)
FROM VisitorCountry VC, Visit V, Time T, AccommodationType AT
WHERE V.id_country = VC.id_country AND T.id_time = V.id_time AND AT.id_type = V.id_type
GROUP BY ROLLUP(VC.visitor_continent, T.year, AT.name)
