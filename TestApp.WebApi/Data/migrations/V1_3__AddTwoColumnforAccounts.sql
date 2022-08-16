create table Test
(
    test_id serial primary key ,
    question varchar(255) not null,
    Options text[] not null,
    answer varchar(200)
);

create table Result
(
    test_id int not null ,
    score int not null,
    TimeTaken int not null,
    foreign key (test_id)
        references Test(test_id)
);
    