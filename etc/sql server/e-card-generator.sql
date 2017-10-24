drop table cards;
drop table card_templates;

create table card_templates
(
	id			int				identity(1,1),
	name		varchar(255)	not null,
	imageName	varchar(255) 	not null,
	fontColor	varchar(100)	not null,
		
	constraint pk_card_templates primary key (id)
);


create table cards
(
	id			int				identity(1,1),
	template_id	int				not null,
	to_email	varchar(100)	not null,
	to_name		varchar(100)	not null,
	message		varchar(100)	not null,
	from_name	varchar(100)	not null,
	from_email	varchar(100)	not null,

	constraint pk_cards primary key(id),
	constraint fk_cards_card_templates foreign key (template_id) references card_templates(id)
);


insert into card_templates values ('Blue Card', 'blue.jpg', '#ffffff');
insert into card_templates values ('Green Card', 'green.jpg', '#ffffff');
insert into card_templates values ('Grey Card', 'light-grey.jpg', '#000000');
insert into card_templates values ('Salmon Card', 'salmon.jpg', '#ffffff');

insert into cards values (1, 'john@xyz.com', 'John', 'Get well soon!', 'Jack', 'jack@abc.com');
