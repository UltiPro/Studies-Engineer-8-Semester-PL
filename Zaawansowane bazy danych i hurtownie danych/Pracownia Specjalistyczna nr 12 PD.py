import csv
import pymongo
from pymongo import MongoClient

client = MongoClient('localhost', 27017)
db = client['IMDB']


"""def zad1(db):
    title_count = db.Title.count_documents({})
    rating_count = db.Rating.count_documents({})
    name_count = db.Name.count_documents({})
    return title_count, rating_count, name_count

title_count, rating_count, name_count = zad1(db)
print(f"Zadanie 1:\nDokumenty w kolekcji Title: {title_count} || Dokumenty w kolekcji Rating: {rating_count} || Dokumenty w kolekcji Name: {name_count}")"""


"""def zad2(db): 
    query = {
        'startYear': 2020, 
        'genres': {'$regex': 'Romance'},
        'runtimeMinutes': {'$gt': 90, '$lte': 120}
    }
    projection = {'primaryTitle': 1, 'startYear': 1, 'genres': 1, 'runtimeMinutes': 1, '_id': 0}
    results = list(db.Title.find(query, projection).sort('primaryTitle', 1).limit(4))
    total_count = db.Title.count_documents(query)
    return results, total_count

romance_movies, total_romance_movies = zad2(db)
print("Zadanie 2:\nFilmy Romance z 2020r, trwajace 90- 120min i uporządkowane według tytułu rosnąco:")
print(f"\na) Pierwsze 4 dokumenty\n{romance_movies[0]},\n{romance_movies[1]},\n{romance_movies[2]},\n{romance_movies[3]}")
print(f"\nb) Liczba wszystkich takich filmów: {total_romance_movies}")"""


"""def zad3(db):
    pipeline = [
        {'$match': {'startYear': 2000}},
        {'$group': {'_id': '$titleType', 'count': {'$sum': 1}}}
    ]
    results = list(db.Title.aggregate(pipeline))
    return results

movies_by_type = zad3(db)
print("Zadanie 3\nTyle filmów różnego typu było wprodukowanych w roku 2000:")
for movie_type in movies_by_type:
    print(movie_type)"""

"""def zad4(db):
    # Pipeline do pobrania 5 najlepszych filmów dokumentalnych
    pipeline = [
        {'$match': {'$and': [{'startYear': {'$gte': 2010, '$lte': 2012}}, {'genres': {'$regex': 'Documentary'}}]}},
        {'$lookup': {'from': 'Rating', 'localField': 'tconst', 'foreignField': 'tconst', 'as': 'ratings'}},
        {'$unwind': '$ratings'},
        {'$project': {'primaryTitle': 1, 'startYear': 1, 'averageRating': {'$toDouble': '$ratings.averageRating'}, '_id': 0}},
        {'$sort': {'averageRating': -1}}
    ]

    results = list(db.Title.aggregate(pipeline))  
    total_count = len(results)
    top_5_results = results[:5]
    return top_5_results, total_count

documentaries, total_documentaries = zad4(db)

print("Zadanie 4\nŚrednia ocena filmów dokumentalnych wprodukowanych w latach 2010-2012.") 
print("Tytuł filmu, rok produkcji oraz jego średnią ocenę. Dane uporzadkowane malejąco wg średniej oceny.")

print(f"\na) Wszystkich takich dokumentów: {total_documentaries}")

print(f"\nb) 5 pierwszych dokumentów spełniających powyższe warunki:")
for doc in documentaries:
    print(doc)"""

# wykonywało się 1,5h co zmusiło autorów do wykonywania innych zapytań w  odzielnym pliku xd.py. 
# Z względu na długi czas oczekiwania przerwano działanie ctrl+c 


"""def zad5(db):
    # Utwórz indeks tekstowy dla pola primaryName
    db.Name.create_index([('primaryName', 'text')], default_language='none')

    query_fonda = {'$text': {'$search': 'Fonda'}}
    query_coppola = {'$text': {'$search': 'Coppola'}}
  
    projection = {'primaryName': 1, 'primaryProfession': 1, '_id': 0}
    
    results_fonda = list(db.Name.find(query_fonda, projection))
    results_coppola = list(db.Name.find(query_coppola, projection))

    
    results = {doc['primaryName']: doc for doc in results_fonda + results_coppola}.values()
    results = list(results)[:5]  

    total_count = len(results_fonda) + len(results_coppola)

    return results, total_count

names, total_names = zad5(db)
print("Zadanie 5\n ")
print(f"Tyle takich dokumentów zwróci zapytanie: {total_names}")
print(f"Tylko 5 pierwszych dokumentów:")
for name in names:
    print(name)"""

"""def zad6(db):
    db.Name.create_index([('birthYear', -1)])
    indexes = list(db.Name.list_indexes())

    print("Lista indeksów w kolekcji Name:")
    for i in indexes:
        print(i)

    print(f"\nTyle indeksów posiada ta kolekcja: {len(indexes)}")
    return (indexes)

indexes = zad6(db)"""



"""def zad7(db):
    # Znajdź filmy z najwyższą oceną = 10.0
    highest_rated_movies = list(db.Rating.find({'averageRating': 10.0}, {'tconst': 1, '_id': 0}))

    # dodaj pole 'max' = 1
    for movie in highest_rated_movies:
        db.Title.update_one({'tconst': movie['tconst']}, {'$set': {'max': 1}})

zad7(db)"""

"""def zad8(db, title, year):
    pipeline = [
        {'$match': {'primaryTitle': title, 'startYear': year}},
        {'$lookup': {'from': 'Rating', 'localField': 'tconst', 'foreignField': 'tconst', 'as': 'ratings'}},
        {'$unwind': '$ratings'},
        {'$project': {'primaryTitle': 1, 'startYear': 1, 'averageRating': '$ratings.averageRating', '_id': 0}}
    ]
    results = list(db.Title.aggregate(pipeline))
    return results

movie_title = "Pauvre Pierrot"
movie_year = 1892
average_rating = zad8(db, movie_title, movie_year)
print(average_rating)"""


"""def zad9(db):
    rating_info = db.Rating.find_one({'tconst': 'tt0083658'}, {'averageRating': 1, 'numVotes': 1, '_id': 0})
    if rating_info:
        db.Title.update_one(
            {'primaryTitle': 'Blade Runner', 'startYear': 1982},
            {'$set': {'rating': [rating_info]}}
        )

zad9(db)
print(f"po dodaniu pola rating: {db.Title.find_one({'primaryTitle': 'Blade Runner', 'startYear': 1982}, {'_id': 0})}")
"""

"""def zad10(db):
    additional_rating = {'averageRating': 10, 'numVotes': 55555}
    db.Title.update_one(
        {'primaryTitle': 'Blade Runner', 'startYear': 1982},
        {'$push': {'rating': additional_rating}}
    )

zad10(db)
print(f"po update pola rating: {db.Title.find_one({'primaryTitle': 'Blade Runner', 'startYear': 1982}, {'_id': 0})}")
"""


"""def zad11(db):
    db.Title.update_one(
        {'primaryTitle': 'Blade Runner', 'startYear': 1982},
        {'$unset': {'rating': ''}}
    )

zad11(db)
print(f"po usunięciu pola rating: {db.Title.find_one({'primaryTitle': 'Blade Runner', 'startYear': 1982}, {'_id': 0})}")
"""


"""def zad12(db):
    db.Title.update_one(
        {'primaryTitle': 'Pan Tadeusz', 'startYear': 1999},
        {'$set': {'avgRating': 9.1}},
        upsert=True
    )

print(f"Pan Tadeusz: {db.Title.find_one({'primaryTitle': 'Pan Tadeusz', 'startYear': 1999}, {'_id': 0})}")

zad12(db)

print(f"\nPan Tadeusz:\n {db.Title.find_one({'primaryTitle': 'Pan Tadeusz', 'startYear': 1999}, {'_id': 0})}")"""

def zad13(db):
    delete_result = db.Title.delete_many({'startYear': {'$lt': 1989}})
    return delete_result.deleted_count

deleted_count = zad13(db)
print(f"Usunięto tyle dokumentów, filmów wyprodukowanych przed 1989r : {deleted_count}")
