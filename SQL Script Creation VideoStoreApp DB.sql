CREATE DATABASE VIDEOSTORE

CREATE TABLE Customer(
	Customer_id int not null primary key identity(1,1),
	Dni char (9) not null,
	Name nvarchar (20) not null,
	LastName nvarchar (20) not null,
	Birthday date not null,
	Mail nvarchar (35 )not null,
	Password nvarchar (35) not null
)


CREATE TABLE Film (
	Film_id int not null primary key identity(1,1),
	Title nvarchar (20) not null, 
	Synopsis nvarchar (500) not null,
	RecommendedAge int not null,
	Avaliable bit not null
)


CREATE TABLE Reservation(
	Reservation_id int not null primary key identity (1,1),
	RentDay Date,
	ReturnDay Date,
	MaxReturnDate Date,
	Customer_id int not null,
	Film_id int not null,
	foreign key (Customer_id) references Customer (Customer_id),
	foreign key (Film_id) references Film (Film_id)
)

INSERT INTO Film (Title, Synopsis, RecommendedAge, Available) VALUES ('Interestellar', 'Seeing that life on Earth is coming to an end, a group of explorers led by pilot Cooper (McConaughey) and scientist Amelia (Hathaway) embark on a mission that may be the most important in the history of mankind: to travel beyond our galaxy to discover a planet in another that can ensure the future of the human race.', 7,0)

INSERT INTO Film (Title, Synopsis, RecommendedAge, Available) VALUES ('Seven', 'The veteran lieutenant Somerset (Morgan Freeman), from the homicide department, is about to retire and be replaced by the ambitious and impulsive detective David Mills (Brad Pitt). Both will have to collaborate in solving a series of murders committed by a psychopath based on the relationship of the seven deadly sins.', 18,0)

INSERT INTO Film (Title, Synopsis, RecommendedAge, Available) VALUES ('Memento', 'Memento chronicles two separate stories of Leonard, an ex-insurance investigator who can no longer build new memories, as he attempts to find the murderer of his wife, which is the last thing he remembers. One story line moves forward in time while the other tells the story backwards revealing more each time', 13,0)

INSERT INTO Film (Title, Synopsis, RecommendedAge, Available) VALUES ('Vanilla Sky', 'A self-indulgent and vain publishing magnate finds his privileged life upended after a vehicular accident with a resentful lover.', 13,0)

INSERT INTO Film (Title, Synopsis, RecommendedAge, Available) VALUES ('Donnie Darko', 'Taking place during an election year in the late 1980s, this movie tells the story of a troubled teenager who receives disturbing visions from a tall bunny rabbit telling him the world will soon come to an end. Seeking answers, Donnie investigates time travel in an attempt to turn back the clock and prevent the worlds seemingly impending doom, actions which pose bizarre and life-changing results.', 18,0)

INSERT INTO Film (Title, Synopsis, RecommendedAge, Available) VALUES ('The Lion King', 'A young lion prince is cast out of his pride by his cruel uncle, who claims he killed his father. While the uncle rules with an iron paw, the prince grows up beyond the Savannah, living by a philosophy: No worries for the rest of your days. But when his past comes to haunt him, the young prince must decide his fate: Will he remain an outcast or face his demons and become what he needs to be?', 0,0)