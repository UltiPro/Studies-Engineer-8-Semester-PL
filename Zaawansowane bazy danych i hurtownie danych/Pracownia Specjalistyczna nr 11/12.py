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

### PS 12

# Zad 1
# db.Title.insertOne({title:'Na zachodzie bez zmian', year: 2022, director:'Edward Berger'});
# db.Title.insertOne({title:'Io', year: 2022, director:'Jerzy Skolimowski'}, {title:'Elvis', year: 2022, director:'Baz Luhrmann'});

# db.Title.insertMany([{title:'Na zachodzie bez zmian', year: 2022, director:'Edward Berger'}, {title:'Io', year: 2022, director:'Jerzy Skolimowski'}, {title:'Elvis', year: 2022, director:'Baz Luhrmann'}])

# Zad 2
# db.Title.deleteMany({year: 2022});

# Zad 3
# db.Title.updateMany({titleType: "movie", startYear: 2020}, {$set: {category: []}})

# Zad 4
# db.Title.updateMany({titleType: "movie", startYear: 2020, genres:{$regex:"Drama"}}, {$push: {category: ["Drama"]}});
# db.Title.updateMany({titleType: "movie", startYear: 2020, genres:{$regex:"Comedy"}}, {$push: {category: ["Comedy"]}});

# Zad 5
# db.Title.aggregate({$match: {titleType:"movie", startYear:{$in:[2017,2018,2019]}}}, {$group:{_id:"$startYear", total:{$sum:1}}});

# Zad 6
# db.Title.aggregate({$match: {titleType:"movie", startYear:{$in:[2017,2018,2019]}}},
# {$group:{_id:"$startYear", total:{$sum:1}}}, {$match: {total: {$gt: 14000}}},{$sort: {total:1}});

# Zad 7
"""db.Title.aggregate(
    {$match: {titleType: "movie", startYear: 2020}},
    {$group: {_id: "$category", total: {$sum: 1}}}, 
    {$sort: {total: 1}}
)

db.Title.aggregate(
    {$match: {titleType: "movie", startYear: 2020}},
    {$unwind: "$category"},
    {$group: {_id: "$category", total: {$sum: 1}}}, 
    {$sort: {total: 1}}
)"""

# Zad 12
# db.Title.createIndex({genres: "text"})
# db.Title.find({titleType:"movie", $text: {$search: "Comedy Romance"}},
#              {_id: 0, genres: 1, primaryTitle: 1})
