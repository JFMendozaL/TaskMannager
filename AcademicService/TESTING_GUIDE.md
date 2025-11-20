# 🎯 AcademicService - Complete Test Guide

## 📝 Testing Order (Recommended Flow)

Follow this order to avoid foreign key constraint errors:

### 1. Health Check
```
GET /api/home/health
```

### 2. Create Subjects
```
POST /api/subjects
{
  "name": "Matemáticas",
  "description": "Álgebra y Geometría",
  "code": "MAT-101",
  "colorCode": "#3B82F6"
}

POST /api/subjects
{
  "name": "Español",
  "description": "Literatura y Gramática",
  "code": "ESP-101",
  "colorCode": "#EF4444"
}
```

### 3. Create Groups
```
POST /api/groups
{
  "name": "4to A",
  "schoolYear": 4,
  "level": "Secundaria"
}

POST /api/groups
{
  "name": "5to A",
  "schoolYear": 5,
  "level": "Secundaria"
}
```

### 4. Create Academic Period
```
POST /api/academic-periods
{
  "name": "Primer Trimestre 2024",
  "type": "trimestre",
  "startDate": "2024-01-15T00:00:00",
  "endDate": "2024-04-15T00:00:00",
  "schoolYear": 2024
}
```

### 5. Activate Period
```
POST /api/academic-periods/1/activate
```

### 6. Create Teacher Assignment
**Note**: Requires valid `teacherId` from UserService
```
POST /api/teacher-assignments
{
  "teacherId": 1,
  "subjectId": 1,
  "groupId": 1,
  "academicPeriodId": 1,
  "startDate": "2024-01-15T00:00:00"
}
```

### 7. Enroll Student
**Note**: Requires valid `studentId` from UserService
```
POST /api/student-enrollments
{
  "studentId": 1,
  "groupId": 1,
  "enrollmentNumber": 1
}
```

### 8. Link Parent to Student
**Note**: Requires valid `parentId` and `studentId` from UserService
```
POST /api/parent-links
{
  "parentId": 1,
  "studentId": 1,
  "relationship": "padre"
}
```

---

## 🔍 Query Examples

### Get Teacher's Assignments
```
GET /api/teacher-assignments/teacher/1
```

### Get Student's Groups
```
GET /api/student-enrollments/student/1
```

### Get Parent's Children
```
GET /api/parent-links/parent/1
```

### Get Group Students
```
GET /api/student-enrollments/group/1
```

### Get Period Assignments
```
GET /api/teacher-assignments/period/1
```

### Get Active Groups
```
GET /api/groups/active
```

### Get Groups by Level
```
GET /api/groups/level/Secundaria
```

### Get Periods by School Year
```
GET /api/academic-periods/school-year/2024
```

### Get Current Period
```
GET /api/academic-periods/current
```

---

## 🧪 Complete Test Scenarios

### Scenario 1: Complete School Setup
1. Create 5 subjects (Matemáticas, Español, Ciencias, Historia, Inglés)
2. Create 3 groups (4to A, 4to B, 5to A)
3. Create academic period (Primer Trimestre 2024)
4. Activate the period
5. Assign 3 teachers to different subject-group combinations
6. Enroll 10 students across the groups
7. Link 5 parents to students

### Scenario 2: Teacher Schedule
1. Create teacher assignments for all their classes
2. Query all assignments for teacher
3. Update one assignment (change end date)
4. Verify updated schedule

### Scenario 3: Student Enrollment
1. Enroll student in group
2. Verify enrollment
3. Get all students in group
4. Update enrollment number
5. Remove student from group

### Scenario 4: Academic Period Transition
1. Create new period
2. Get current active period
3. Activate new period (automatically deactivates old)
4. Verify old period is inactive
5. Create new assignments for new period

---

## ⚠️ Common Errors & Solutions

### Error: Foreign Key Constraint
**Problem**: Trying to create assignment/enrollment with non-existent IDs
**Solution**: Ensure referenced entities exist first (subjects, groups, periods, users)

### Error: Duplicate Entry
**Problem**: Trying to create duplicate assignment or enrollment
**Solution**: Check existing records first or update existing one

### Error: Invalid Date Range
**Problem**: End date before start date in academic period
**Solution**: Ensure endDate > startDate

### Error: Subject Code Already Exists
**Problem**: Trying to create subject with existing code
**Solution**: Use unique code or update existing subject

---

## 📊 Expected Responses

### Success Response (200/201)
```json
{
  "data": { /* entity data */ },
  "message": "Operation completed successfully",
  "success": true,
  "errors": null
}
```

### Error Response (400/404)
```json
{
  "data": null,
  "message": "Error message",
  "success": false,
  "errors": ["Detailed error"]
}
```

---

## 🔗 Integration Testing

### With UserService
1. Create users (Teachers, Students, Parents) in UserService
2. Use their IDs in Academic Service endpoints
3. Verify cross-service references work

### With TaskService
1. Create assignments in AcademicService
2. Create tasks linked to TeacherSubjectGroup IDs
3. Verify task-assignment relationship

---

## 📈 Performance Testing

### Load Test Scenarios
- 100 concurrent subject creation requests
- 1000 student enrollments
- 500 teacher assignments
- Query optimization for teacher schedules

---

## ✅ Validation Checklist

- [ ] All CRUD operations work for each entity
- [ ] Validations prevent duplicates
- [ ] Foreign key constraints enforced
- [ ] Active period management works
- [ ] Queries with filters return correct data
- [ ] Error messages are clear and helpful
- [ ] Swagger documentation is accurate
- [ ] All endpoints return ApiResponse format

---

**Happy Testing! 🚀**
