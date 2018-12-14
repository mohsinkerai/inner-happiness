# Backend Sub-Project

To be Made via Spring-Boot

It Depends on MySQL for its data storage, and thats it.

For Schema Migration, it uses Liquibase.

mvn spring-boot:run to run this project.

src/resources/application.yml for confiugration properties

Added Authentication


Cycle will work like

Open -> Appointed -> Closed
Open -> Appointed -> Midterm -> Appointed -> Closed
Open -> Appointed -> Midterm -> Canceled -> Closed

AppointmentPosition Work Like this
Open -> Appointed -> Retired

## Things todo:
* You Can't Nominate/Recommend Anyone if Position is Appointed
* You Can't Move Cycle State to Appointed until and unless all positions in that cycle have one recommendation
  * In this process, you also need to mark those recommended position to appointed
* In order for mid-term appointed:
  * Cycle should be in appointed state
  * You need to specify re-appointment positions with year served upfront.
  * You should be able to add more positions during midterm phase.
* A Midterm Appointed can be cancelled too.