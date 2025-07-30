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

    [ ] vereist een lijst van coach-competenties.
    [ ] wordt begeleid door exact één coach.
<!-- Testen -->

**Domeinvalidatie: Course, Availability**
    - course.AddTimeSlotList() + Availabilty.DoesCourseHaveTimeslots() 
    [ ] is ongeldig zolang er geen lesmomenten zijn toegevoegd.
    *via hasSchedule*

    - course.CheckingCoach()
    [ ] kan pas een coach toegewezen krijgen nadat de opleiding als geldig en definitief is bevestigd.
    *via StatusCourse, hasCoach*

    - course.AddTimeSlotlist()
    [ ] laat geen wijzigingen aan het lesrooster meer toe zodra een coach is toegewezen.
    *via HasCoach*

*Coaches*
**Domeinobjecten: Coach en Skill**
    [ ] beschikt over een lijst van skills (competenties).


**Domeinvalidatie: Course, Availability**
    - course.CheckingCoach() 
        - via Availabilty.CheckingCoachByStatus()
            Availabilty.CheckCoachCompetencesForCourse() 
            Availabilty.CheckCoachAvailabilty()
    [ ] is slechts geschikt voor opleidingen waarvoor hij of zij alle vereiste competenties bezit.
    [ ] kan niet worden toegewezen aan overlappende opleidingen en moet dus beschikbaar zijn op de ingeplande momenten.

*Interaction in Domain*
    - new Coach(name, email)
[x] Coach registreren: maak een coach aan met naam en e-mailadres.
<!-- CoachTest: Coach_Can_Be_Added -->


    - coach methodes: AddCompetence(), AddCompetenceList(), RemoveCompetence()
[x] Coach Competenties toevoegen/verwijderen
<!-- CoachTest: Coach_Can_Add_Skills -->
<!-- CoachTest: Coach_Can_Remove_Skills -->
<!-- CoachTest: Coach_Can_Add_List_Of_Skills_And_Removes_Old_Skills -->

    - new Course(name, startDate, endDate)
[x] Cursus aanmaken: maak een lege cursus aan met naam en periode.
<!-- CourseTest: Course_Can_Add_Basic_Course -->

    - course methodes: AddCompetenceList(), AddTimeSlotToCourse(), AddTimeSlotList()
[x] Cursus Competenties toevoegen/verwijderen
[x] Cursus Lesmomenten toevoegen/verwijderen (dag, beginuur, einduur).
<!-- CourseTest: Course_Can_Add_And_Remove_Skills -->
<!-- CourseTest: Course_Can_Add_And_Remove_Timeslots -->
<!-- CourseTest: Coach_Can_Add_List_Of_Timeslots -->

    - course methode: ValidateCourseBasedOnTimeslots()
    [ ] Cursus bevestigen: markeer de cursus als geldig en definitief, mits hij aan alle voorwaarden voldoet (inclusief minstens één lesmoment).
<!-- CourseTest: Course_Confirmed_When_Timeslot_Added -->
<!-- CourseTest: Course_Not_Confirmed_When_No_Timeslot_Added -->

    - course methode: CheckingCoach()
    [ ] Coach toewijzen: wijs een coach toe, enkel indien de cursus bevestigd is en de coach geschikt én beschikbaar is.
