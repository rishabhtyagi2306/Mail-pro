<img src="/images/LOGO2.png" width="15" height="15"> # Mail-pro
A mailing app that helps you to send mails to a specified group of people created by you by using beautiful, predefined templates. This web app is designed specially for teachers for sending mails to a group of students.
The working of the app will be as follows:
1. Login using forms authentication.
2. Signup using email verification i.e. a mail will be sent which will contain the activation link.
3. Teachers can add students individually or through excel sheets.
4. They can create categories of the students according to their choice.
5. All the details of the students can be deleted and edited throught the portal.
6. Faculties can send mails to selected students of a group or whole group. The mails can also be sent to multiple groups at the same time.
7. Faculties can send mails using templates that will be uploaded by them. The body and name of that recipient will be rendered in the mail.
8. For writing mails used the text editor Tinymce.
9. There are few additional features like forgot password and reset password.

## Technology Stack

1. ASP.NET (MVC) version - 4.5
2. Microsoft SQL Server Management Studio for managing databases.
3. HTML
4. CSS
5. Javascript
6. BootStrap

## Screenshots

### Sign In page (Landing page).
![Image](/images/Sign_in.png)


### Inbox.
![Image](/images/Sent_mails.png)


### View Categories that are created by the faculty.
![Image](/images/ViewCategory.png)


### Add Student using the form or using excel sheet.
![Image](/images/Add_Student.png)


### View added students list.
![Image](/images/View_student.png)


### Select templates according to your needs from soo many given beautiful templates.
![Image](/images/Template.png)


### Compose mail using the editor.
![Image](/images/Compose.png)

## Details

This mailing is developed using asp.net (mvc) using Entity Framework 6.0. It is developed for the ease of faculty to communicate with students and manage their records. It's easy to understand UI/UX helps faculties to send mails and manage Student record effectively. This app uses concept like forms authentication, async await and Multithreading. For sending mails it uses SMTP server (port 587). Instead of using lambda expressions it has direct SQL commands to querry data from the database for better understanding of SQL.