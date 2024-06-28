DROP TYPE IF EXISTS Teacher;
CREATE TABLE Teacher
(
    Id UUID PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,   
    Birth DATE NOT NULL,
    Specialty VARCHAR(255) NOT NULL,
    Workload INT NOT NULL,    
    WorkPeriod VARCHAR(50) NOT NULL
);