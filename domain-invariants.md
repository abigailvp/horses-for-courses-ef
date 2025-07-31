*Courses*
**Domeinobjecten: Course en Timeslot**
[x] Heeft een naam.
[x] wordt ingepland over een bepaalde periode met een start- en einddatum.
<!-- CourseTest: Course_Can_Add_Basic_Course -->

[x] heeft vaste lesmomenten, bijvoorbeeld op maandag en woensdag van 10u tot 12u.
[x] heeft enkel les op weekdagen (maandag t.e.m. vrijdag).
[x] plant lessen uitsluitend binnen de kantooruren (tussen 9u00 en 17u00).
[x] moet in totaal minstens één uur duren.
<!-- CourseTest: Course_Can_Add_Timeslot -->
<!-- CourseTest: Cant_Add_Timeslot_on_sunday -->
<!-- CourseTest: Cant_Add_Timeslot_before_9 -->
<!-- CourseTest: Cant_Add_Timeslot_with_duration_smaller_Than_1 -->

[x] vereist een lijst van coach-competenties.
[x] wordt begeleid door exact één coach.
*via bools hasSchedule hasCoach die ingesteld worden in:*
    - Course.AddTimeSlotList() of Course.AddTimeSlotToCourse()
    - Course.AddingCoach()

**Domeinvalidatie: Course, Availability** 
[x] is ongeldig zolang er geen lesmomenten zijn toegevoegd.
    - Course.AddingCoach() gebruikt
        - Availabilty.DoesCourseHaveTimeslots()
        - Availability.CheckingCoachByStatus() gebruikt
            -Availabilty.DoesCourseHaveTimeslots()
            -Course.COnflictsWith() + Course.Overlaps()

[x] kan pas een coach toegewezen krijgen nadat de opleiding als geldig en definitief is bevestigd.
     - course.AddingCoach()

[x] laat geen wijzigingen aan het lesrooster meer toe zodra een coach is toegewezen.
    - course.AddTimeSlotlist()



*Coaches*
**Domeinobjecten: Coach en Skill**
[x] beschikt over een lijst van skills (competenties).


**Domeinvalidatie: Course, Availability**
[x] is slechts geschikt voor opleidingen waarvoor hij of zij alle vereiste competenties bezit.
[x] kan niet worden toegewezen aan overlappende opleidingen en moet dus beschikbaar zijn op de ingeplande momenten.
    - course.AddingCoach() 


**Interaction in Domain**
[x] Coach registreren: maak een coach aan met naam en e-mailadres.
  - new Coach(name, email)
<!-- CoachTest: Coach_Can_Be_Added -->


[x] Coach Competenties toevoegen/verwijderen
  - coach methodes: AddCompetence(), AddCompetenceList(), RemoveCompetence()
<!-- CoachTest: Coach_Can_Add_Skills -->
<!-- CoachTest: Coach_Can_Remove_Skills -->
<!-- CoachTest: Coach_Can_Add_List_Of_Skills_And_Removes_Old_Skills -->


[x] Cursus aanmaken: maak een lege cursus aan met naam en periode.
- new Course(name, startDate, endDate)
<!-- CourseTest: Course_Can_Add_Basic_Course -->


[x] Cursus Competenties toevoegen/verwijderen
[x] Cursus Lesmomenten toevoegen/verwijderen (dag, beginuur, einduur).
  - course methodes: AddCompetenceList(), AddTimeSlotToCourse(), AddTimeSlotList()
<!-- CourseTest: Course_Can_Add_And_Remove_Skills -->
<!-- CourseTest: Course_Can_Add_And_Remove_Timeslots -->
<!-- CourseTest: Coach_Can_Add_List_Of_Timeslots -->

 
[x] Cursus bevestigen: markeer de cursus als geldig en definitief, mits hij aan alle voorwaarden voldoet (inclusief minstens één lesmoment).
    - course methode: ValidateCourseBasedOnTimeslots()
<!-- CourseTest: Course_Confirmed_When_Timeslot_Added -->
<!-- CourseTest: Course_Not_Confirmed_When_No_Timeslot_Added -->

   
[x] Coach toewijzen: wijs een coach toe, enkel indien de cursus bevestigd is en de coach geschikt én beschikbaar is.
     - course methode: AddingCoach()
<!-- CourseTest: Coach_Added_When_Course_HasSchedule_And_Coach_No_Other_Course -->
<!-- CourseTest: Coach_Not_Added_When_Course_No_Schedule -->
<!-- CourseTest: Coach_Not_Added_When_Course_No_Required_Skills -->
<!-- CourseTest: Coach_Not_Added_When_Coach_Has_Overlapping_Course -->
<!-- CourseTest: Coach_Added_When_Schedule_And_Coach_Has_Not_Overlapping_Course -->
