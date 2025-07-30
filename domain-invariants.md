*Courses*
**Domeinobjecten: Course en Timeslot**
    [ ] Heeft een naam.
    [ ] wordt ingepland over een bepaalde periode met een start- en einddatum.
    [ ] heeft vaste lesmomenten, bijvoorbeeld op maandag en woensdag van 10u tot 12u.
    [ ] heeft enkel les op weekdagen (maandag t.e.m. vrijdag).
    [ ] plant lessen uitsluitend binnen de kantooruren (tussen 9u00 en 17u00).
    [ ] moet in totaal minstens één uur duren.
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
    [ ] Coach registreren: maak een coach aan met naam en e-mailadres.

    - coach methodes: AddCompetence(), AddCompetenceList(), RemoveCompetence()
    [ ] Coach Competenties toevoegen/verwijderen

    - new Course(name, startDate, endDate)
    [ ] Cursus aanmaken: maak een lege cursus aan met naam en periode.

    - course methodes: AddCompetenceList(), AddTimeSlotToCourse(), AddTimeSlotList()
    [ ] Cursus Competenties toevoegen/verwijderen
    [ ] Cursus Lesmomenten toevoegen/verwijderen (dag, beginuur, einduur).

    - course methode: ValidateCourseBasedOnTimeslots()
    [ ] Cursus bevestigen: markeer de cursus als geldig en definitief, mits hij aan alle voorwaarden voldoet (inclusief minstens één lesmoment).

    - course methode: CheckingCoach()
    [ ] Coach toewijzen: wijs een coach toe, enkel indien de cursus bevestigd is en de coach geschikt én beschikbaar is.
