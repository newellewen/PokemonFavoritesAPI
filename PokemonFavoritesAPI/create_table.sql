CREATE TABLE users
(
	id 				INTEGER 	PRIMARY KEY AUTOINCREMENT
	, first_name 	VARCHAR(50)
	, last_name 	VARCHAR(50)
	, email 		VARCHAR(100)
	, username 		VARCHAR(50)
	, password 		VARCHAR(50)
);

CREATE TABLE favorite_pokemon
(
	id 				INTEGER	PRIMARY KEY AUTOINCREMENT
	, user_id		INTEGER
	, pokemon_id	INTEGER
	, name 			VARCHAR(50)
	, types  		VARCHAR(50)
	, thumbnail 	VARCHAR(50)
	, FOREIGN KEY (user_id) REFERENCES users(id)
);
