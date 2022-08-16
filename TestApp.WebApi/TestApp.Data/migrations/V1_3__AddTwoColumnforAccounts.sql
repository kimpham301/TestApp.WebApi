alter table accounts
    add column roles int not null;

alter table accounts
    add column tokens varchar(500) not null;
    

    