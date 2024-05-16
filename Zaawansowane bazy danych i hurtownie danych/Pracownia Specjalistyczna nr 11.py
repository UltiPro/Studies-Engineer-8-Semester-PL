from pymongo import MongoClient


def get_database():
    return MongoClient(
        "mongodb+srv://ultiprogames:<password>@cluster0.vihtqp9.mongodb.net/"
    )["IMDB"]


def get_local_database():
    return MongoClient("127.0.0.1:27017")["IMDB"]


if __name__ == "__main__":
    db = get_local_database()

# Zad 1 poza VSCode
# Zad 2 poza VSCode (w Compass'ie)

# Zad 3
# NOT DONE

###!!! use [nazwa_bazy] !!!###

# Zad 4
# db.Rating.count()
# db.Title.count()

# Zad 5
# db.Title.find().skip(29).limit(1)

# Zad 6
# db.Title.find().limit(100).sort({startYear:-1})

# Zad 7
# db.Title.find({startYear: 1994, genres:"Drama", runtimeMinutes:{$gt:100}}, {primaryTitle:1, startYear:1, runtimeMinutes:1, genres:1, _id:0}).limit(10)

# Zad 8
# db.Title.find({startYear: 2000, genres:{$regex:"Animation"}, runtimeMinutes:{$lte:10}}, {primaryTitle:1, startYear:1, runtimeMinutes:1, genres:1, _id:0}).limit(10)

# Zad 9
# db.Title.find({$or:[{startYear: 2000}, {genres:{$regex:"Animation"}}, {runtimeMinutes:{$lte:10}}]}, {primaryTitle:1, startYear:1, runtimeMinutes:1, genres:1, _id:0}).count()

# Zad 10
# db.Title.find({primaryTitle:"Casablanca", startYear:1942},{tconst:1,_id:0})
# tconst: 'tt0034583'
# db.Rating.find({tconst: 'tt0034583'})

# by agregate
# db.Title.aggregate({$match:{primaryTitle:"Casablanca", startYear:1942}}, {$lookup:{from:"Rating", localField:"tconst", foreignField:"tconst", as:"joinRating"}})
