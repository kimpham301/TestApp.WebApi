create table Accounts
(
    user_id serial primary key ,
    email varchar(255) unique not null,
    password varchar(50) not null
);

create table Roles
(
    role_id serial PRIMARY KEY,
    role_name varchar(100) unique not null
);

create table AccountRoles
(
    user_id int not null,
    role_id int not null,
    primary key (user_id, role_id),
    foreign key (role_id)
        references Roles(role_id),
    foreign key (user_id)
        references  Accounts(user_id)
);