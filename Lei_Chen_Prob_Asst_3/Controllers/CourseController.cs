using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lei_Chen_Prob_Asst_3.Data;
using PA3.Models;
using PA3.Services;

namespace Lei_Chen_Prob_Asst_3.Controllers
{
  public class CourseController : Controller
  {
    private readonly MvcCourseContext _context;
    private readonly StudentService _studentService;

    private readonly ILogger<HomeController> _logger;

    public CourseController(MvcCourseContext context, StudentService studentService, ILogger<HomeController> logger)
    {
      _context = context;
      _studentService = studentService;
      _logger = logger;
    }

    // GET: Course
    public async Task<IActionResult> Index(string sortOrder, string searchString, string selectedTerm,
        int? selectedYear, int? programId, int? pageNumber)
    {
      ViewData["CurrentSort"] = sortOrder;
      ViewData["CodeSortParam"] = String.IsNullOrEmpty(sortOrder) ? "code_desc" : "";
      ViewData["TitleSortParam"] = sortOrder == "title" ? "title_desc" : "title";
      ViewData["SectionSortParam"] = sortOrder == "section" ? "section_desc" : "section";
      ViewData["TermSortParam"] = sortOrder == "term" ? "term_desc" : "term";
      ViewData["YearSortParam"] = sortOrder == "year" ? "year_desc" : "year";
      ViewData["ProgramSortParam"] = sortOrder == "program" ? "program_desc" : "program";

      // Maintain filter state
      ViewData["CurrentFilter"] = searchString;
      ViewData["SelectedTerm"] = selectedTerm;
      ViewData["SelectedYear"] = selectedYear;
      ViewData["SelectedProgram"] = programId;

      // Setup paging
      int pageSize = 10;
      int currentPage = pageNumber ?? 1;
      ViewData["CurrentPage"] = currentPage;

      // Initialize Terms dropdown (make sure Terms table exists and has data)
      var terms = await _context.Set<Term>().Select(t => t.Semester).ToListAsync();
      ViewData["Terms"] = new SelectList(terms);

      // Initialize Years dropdown (4 years from current year)
      var currentYear = DateTime.Now.Year;
      var years = Enumerable.Range(currentYear, 4)
          .Select(y => new SelectListItem
          {
            Value = y.ToString(),
            Text = y.ToString(),
            Selected = (selectedYear.HasValue && y == selectedYear.Value)
          });
      ViewData["Years"] = years;

      // Get programs for buttons
      ViewData["Programs"] = await _context.Set<UGProgram>().ToListAsync();

      // Start with all courses
      var courses = _context.Courses
          .Include(c => c.Program)
          .AsQueryable();

      // Apply filters
      if (!string.IsNullOrEmpty(searchString))
      {
        courses = courses.Where(c => c.Title.ToUpper().Contains(searchString.ToUpper()));
      }

      if (!string.IsNullOrEmpty(selectedTerm))
      {
        courses = courses.Where(c => c.Term == selectedTerm);
      }

      if (selectedYear.HasValue)
      {
        courses = courses.Where(c => c.Year == selectedYear.Value);
      }

      if (programId.HasValue)
      {
        courses = courses.Where(c => c.ProgramID == programId.Value);
      }

      switch (sortOrder)
      {
        case "code_desc":
          courses = courses.OrderByDescending(s => s.Code);
          break;
        case "title":
          courses = courses.OrderBy(s => s.Title);
          break;
        case "title_desc":
          courses = courses.OrderByDescending(s => s.Title);
          break;
        case "section":
          courses = courses.OrderBy(s => s.Section);
          break;
        case "section_desc":
          courses = courses.OrderByDescending(s => s.Section);
          break;
        case "term":
          courses = courses.OrderBy(s => s.Term);
          break;
        case "term_desc":
          courses = courses.OrderByDescending(s => s.Term);
          break;
        case "year":
          courses = courses.OrderBy(s => s.Year);
          break;
        case "year_desc":
          courses = courses.OrderByDescending(s => s.Year);
          break;
        case "program":
          courses = courses.OrderBy(s => s.Program.Name);
          break;
        case "program_desc":
          courses = courses.OrderByDescending(s => s.Program.Name);
          break;
        default:
          courses = courses.OrderBy(s => s.Code);
          break;
      }
      // Get total count after filtering
      var totalItems = await courses.CountAsync();
      var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
      ViewData["TotalPages"] = totalPages;

      // Apply paging
      var pagedCourses = await courses
          .Skip((currentPage - 1) * pageSize)
          .Take(pageSize)
          .ToListAsync();

      return View(pagedCourses);
    }

    // GET: Course/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var course = await _context.Courses
          .Include(c => c.Program)
          .FirstOrDefaultAsync(m => m.ID == id);
      if (course == null)
      {
        return NotFound();
      }

      return View(course);
    }

    // GET: Course/Create
    public IActionResult Create()
    {
      ViewData["TermID"] = new SelectList(_context.Set<Term>(), "ID", "Semester");
      ViewData["ProgramID"] = new SelectList(_context.Set<UGProgram>(), "ID", "Name");
      return View();
    }

    // POST: Course/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ID,Code,Title,Section,Term,Year,ProgramID")] Course course)
    {
      if (ModelState.IsValid)
      {
        _context.Add(course);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      ViewData["ProgramID"] = new SelectList(_context.Set<UGProgram>(), "ID", "Name", course.ProgramID);
      ViewData["TermID"] = new SelectList(_context.Set<Term>(), "ID", "Semester");
      return View(course);
    }

    // GET: Course/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var course = await _context.Courses.FindAsync(id);
      if (course == null)
      {
        return NotFound();
      }
      ViewData["ProgramID"] = new SelectList(_context.Set<UGProgram>(), "ID", "Code", course.ProgramID);
      return View(course);
    }

    // POST: Course/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ID,Code,Title,Section,Term,Year,ProgramID")] Course course)
    {
      if (id != course.ID)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(course);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!CourseExists(course.ID))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      ViewData["ProgramID"] = new SelectList(_context.Set<UGProgram>(), "ID", "Code", course.ProgramID);
      return View(course);
    }

    // GET: Course/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var course = await _context.Courses
          .Include(c => c.Program)
          .FirstOrDefaultAsync(m => m.ID == id);
      if (course == null)
      {
        return NotFound();
      }

      return View(course);
    }

    // POST: Course/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var course = await _context.Courses.FindAsync(id);
      if (course != null)
      {
        _context.Courses.Remove(course);
      }

      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool CourseExists(int id)
    {
      return _context.Courses.Any(e => e.ID == id);
    }


    // ================================================================================
    // GET: Student
    public async Task<IActionResult> StudentIndex()
    {
      var mvcStudentContext = _context.Students.Include(s => s.City).Include(s => s.Program);
      return View(await mvcStudentContext.ToListAsync());
    }

    // GET: Student/Details/5
    public async Task<IActionResult> StudentDetails(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var student = await _context.Students
          .Include(s => s.City)
          .Include(s => s.Program)
          .FirstOrDefaultAsync(m => m.StudentID == id);
      if (student == null)
      {
        return NotFound();
      }

      return View(student);
    }

    // GET: Student/Create
    public IActionResult StudentCreate()
    {
      // Populate dropdowns for the create form
      ViewData["CityID"] = new SelectList(_context.Set<City>(), "ID", "Name");
      ViewData["ProgramID"] = new SelectList(_context.Set<UGProgram>(), "ID", "Name");
      var studentTypes = new List<string> { "DOMESTIC", "INTERNATIONAL" };
      ViewData["Types"] = new SelectList(studentTypes);
      // Show fee information - Fixed to handle multiple policies per type
      var feePolicies = _context.FeePolicies
          .GroupBy(fp => fp.Category.StartsWith("DOMESTIC") ? "DOMESTIC" : "INTERNATIONAL")
          .ToDictionary(
              g => g.Key,
              g => g.Min(fp => fp.RegistrationFee)  // Get the minimum registration fee for each type
          );
      ViewData["FeePolicies"] = feePolicies;

      // Initialize new student
      var student = new Student { Status = StudentStatus.ELIGIBLE };
      return View(student);
    }

    // POST: Student/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StudentCreate([Bind("FirstName,LastName,Address,PostalCode,Email,Type,CityID,ProgramID")] Student student)
    {
      if (ModelState.IsValid)
      {
        // Set initial student status
        student.Status = StudentStatus.ELIGIBLE;
        student.Province = _studentService.GetProvince(student);

        // Get appropriate fee policy
        var feePolicy = await _context.FeePolicies
            .FirstOrDefaultAsync(fp => fp.Category.StartsWith(student.Type));

        if (feePolicy == null)
        {
          ModelState.AddModelError("", "Fee policy not found.");
          return View(student);
        }

        // Create financial statement
        var financialStatement = new FinancialStatement
        {
          LastChanged = DateTime.Now,
          Student = student,
          FeePolicy = feePolicy
        };

        // Add registration fee entry
        var registrationEntry = new StatementEntry
        {
          Description = "Registration Fee",
          Value = feePolicy.RegistrationFee,
          FinancialStatement = financialStatement
        };

        // Save everything
        _context.Add(student);
        _context.Add(financialStatement);
        _context.Add(registrationEntry);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(StudentIndex));
      }

      // If we got this far, something failed, redisplay form
      ViewData["CityID"] = new SelectList(_context.Set<City>(), "ID", "Name", student.CityID);
      ViewData["ProgramID"] = new SelectList(_context.Set<UGProgram>(), "ID", "Name", student.ProgramID);
      ViewData["Types"] = new SelectList(new[] { "DOMESTIC", "INTERNATIONAL" }, student.Type);
      return View(student);
    }

    // GET: Student/Edit/5
    public async Task<IActionResult> StudentEdit(int? id)
    {
      if (id == null)
        return NotFound();

      var student = await _context.Students
          .Include(s => s.FinancialStatement)
          .FirstOrDefaultAsync(s => s.StudentID == id);

      if (student == null)
        return NotFound();

      ViewData["CityID"] = new SelectList(_context.Set<City>(), "ID", "Name", student.CityID);
      ViewData["ProgramID"] = new SelectList(_context.Set<UGProgram>(), "ID", "Name", student.ProgramID);
      ViewData["Types"] = new SelectList(new[] { "DOMESTIC", "INTERNATIONAL" }, student.Type);

      return View(student);
    }

    // POST: Student/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StudentEdit(int id, [Bind("StudentID,FirstName,LastName,Address,PostalCode,Email,Type,Status,ProgramID,CityID")] Student student)
    {
      if (id != student.StudentID)
        return NotFound();

      if (ModelState.IsValid)
      {
        try
        {
          // Update province based on city/postal code
          student.Province = _studentService.GetProvince(student);

          // Update financial statement timestamp
          var financialStatement = await _context.FinancialStatements
              .FirstOrDefaultAsync(fs => fs.StudentID == student.StudentID);
          if (financialStatement != null)
          {
            financialStatement.LastChanged = DateTime.Now;
          }

          _context.Update(student);
          await _context.SaveChangesAsync();
          return RedirectToAction(nameof(StudentIndex));
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!StudentExists(student.StudentID))
            return NotFound();
          else
            throw;
        }
      }

      // If we got this far, something failed, redisplay form
      ViewData["CityID"] = new SelectList(_context.Set<City>(), "ID", "Name", student.CityID);
      ViewData["ProgramID"] = new SelectList(_context.Set<UGProgram>(), "ID", "Name", student.ProgramID);
      ViewData["Types"] = new SelectList(new[] { "DOMESTIC", "INTERNATIONAL" }, student.Type);
      return View(student);
    }

    // GET: Student/Delete/5
    public async Task<IActionResult> StudentDelete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var student = await _context.Students
          .Include(s => s.City)
          .Include(s => s.Program)
          .FirstOrDefaultAsync(m => m.StudentID == id);
      if (student == null)
      {
        return NotFound();
      }

      return View(student);
    }

    // POST: Student/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StudentDeleteConfirmed(int id)
    {
      var student = await _context.Students.FindAsync(id);
      if (student != null)
      {
        _context.Students.Remove(student);
      }

      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool StudentExists(int id)
    {
      return _context.Students.Any(e => e.StudentID == id);
    }

    // POST: Student/EnrollInCourse
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EnrollInCourse(int studentId, int courseId)
    {
      var student = await _context.Students.FindAsync(studentId);
      var course = await _context.Courses.FindAsync(courseId);

      if (student == null || course == null)
      {
        return NotFound();
      }

      if (_studentService.IsStudentEligibleToEnroll(student))
      {
        student.Courses.Add(course);
        _studentService.UpdateStudentStatus(student);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Details), new { id = studentId });
      }

      ModelState.AddModelError("", "Student is not eligible to enroll in courses.");
      return RedirectToAction(nameof(Details), new { id = studentId });
    }

    // ==================================================
    // GET: Course/Course/5
    public async Task<IActionResult> CourseManage(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var course = await _context.Courses
          .Include(c => c.Program)
          .FirstOrDefaultAsync(m => m.ID == id);

      if (course == null)
      {
        return NotFound();
      }

      // Get enrolled students for this course
      var enrolledStudents = await _context.Students
          .Where(s => s.Courses.Any(c => c.ID == id))
          .Include(s => s.Courses)
          .ToListAsync();

      // Get eligible students (in same program and not enrolled)
      var eligibleStudents = await _context.Students
          .Where(s => s.ProgramID == course.ProgramID &&
                      !s.Courses.Any(c => c.ID == id) &&
                      (s.Status == StudentStatus.ELIGIBLE || s.Status == StudentStatus.ENROLLED))
          .Select(s => new { Id = s.StudentID, Name = s.FullName })
          .ToListAsync();

      ViewBag.EnrolledStudents = enrolledStudents;
      ViewBag.EligibleStudents = new SelectList(eligibleStudents, "Id", "Name");

      // Store course ID for return navigation
      TempData["CourseId"] = id;

      return View(course);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EnrollStudent(int courseId, int studentId)
    {
      var course = await _context.Courses.FindAsync(courseId);
      var student = await _context.Students
          .Include(s => s.Courses)
          .Include(s => s.FinancialStatement)
              .ThenInclude(fs => fs.Entries)
          .FirstOrDefaultAsync(s => s.StudentID == studentId);

      if (course == null || student == null)
        return NotFound();

      if (!course.IsOpenToEnroll)
      {
        TempData["Error"] = "Course is not open for enrollment.";
        return RedirectToAction(nameof(CourseManage), new { id = courseId });
      }

      if (!_studentService.IsStudentEligibleToEnroll(student))
      {
        TempData["Error"] = "Student is not eligible to enroll.";
        return RedirectToAction(nameof(CourseManage), new { id = courseId });
      }

      // Get appropriate fee policy based on student type and course count
      var courseCount = student.Courses.Count;
      var isFullTime = courseCount >= 2; // Will be full-time after this enrollment
      var policyCategory = $"{student.Type} {(isFullTime ? "Full-Time" : "Part-Time")}";
      var feePolicy = await _context.FeePolicies
          .FirstOrDefaultAsync(fp => fp.Category.ToUpper() == policyCategory.ToUpper());

      if (feePolicy == null)
      {
        TempData["Error"] = "Fee policy not found.";
        return RedirectToAction(nameof(CourseManage), new { id = courseId });
      }

      // Handle different cases
      switch (courseCount)
      {
        case 0: // Case 1: First course
          await HandleFirstCourseEnrollment(student, course, feePolicy);
          break;

        case 1: // Case 2: Second course
          await HandleSecondCourseEnrollment(student, course, feePolicy);
          break;

        case 2: // Case 3: Third course (becoming full-time)
          await HandleThirdCourseEnrollment(student, course, feePolicy);
          break;

        default: // Case 4: Already full-time
          await HandleFullTimeEnrollment(student, course, feePolicy);
          break;
      }

      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(CourseManage), new { id = courseId });
    }

    private async Task HandleFirstCourseEnrollment(Student student, Course course, FeePolicy feePolicy)
    {
      // Create new financial statement
      var financialStatement = new FinancialStatement
      {
        LastChanged = DateTime.Now,
        Student = student,
        FeePolicy = feePolicy
      };

      // Add all fee entries
      financialStatement.Entries = new List<StatementEntry>
    {
        new StatementEntry { Description = "Tuition Fee", Value = feePolicy.TuitionFee },
        new StatementEntry { Description = "Registration Fee", Value = feePolicy.RegistrationFee },
        new StatementEntry { Description = "Facilities Fee", Value = feePolicy.FacilitiesFee },
        new StatementEntry { Description = "Union Fee", Value = feePolicy.UnionFee }
    };

      student.FinancialStatement = financialStatement;
      student.Courses.Add(course);
    }

    private async Task HandleSecondCourseEnrollment(Student student, Course course, FeePolicy feePolicy)
    {
      // Add only tuition fee
      var tuitionEntry = new StatementEntry
      {
        Description = "Tuition Fee",
        Value = feePolicy.TuitionFee,
        FinancialStatement = student.FinancialStatement
      };

      student.FinancialStatement.Entries.Add(tuitionEntry);
      student.Courses.Add(course);
    }

    private async Task HandleThirdCourseEnrollment(Student student, Course course, FeePolicy feePolicy)
    {
      // Create new financial statement for full-time status
      var financialStatement = new FinancialStatement
      {
        LastChanged = DateTime.Now,
        Student = student,
        FeePolicy = feePolicy
      };

      // Add all fees including three tuition fees
      var entries = new List<StatementEntry>
    {
        new StatementEntry { Description = "Registration Fee", Value = feePolicy.RegistrationFee },
        new StatementEntry { Description = "Facilities Fee", Value = feePolicy.FacilitiesFee },
        new StatementEntry { Description = "Union Fee", Value = feePolicy.UnionFee }
    };

      // Add three tuition fees
      for (int i = 0; i < 3; i++)
      {
        entries.Add(new StatementEntry
        {
          Description = "Tuition Fee",
          Value = feePolicy.TuitionFee
        });
      }

      financialStatement.Entries = entries;
      student.FinancialStatement = financialStatement;
      student.Courses.Add(course);
    }

    private async Task HandleFullTimeEnrollment(Student student, Course course, FeePolicy feePolicy)
    {
      // Add only tuition fee
      var tuitionEntry = new StatementEntry
      {
        Description = "Tuition Fee",
        Value = feePolicy.TuitionFee,
        FinancialStatement = student.FinancialStatement
      };

      student.FinancialStatement.Entries.Add(tuitionEntry);
      student.Courses.Add(course);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EnrollStudent02(int courseId, int studentId)
    {
      var course = await _context.Courses.FindAsync(courseId);
      var student = await _context.Students
          .Include(s => s.Courses)
          .FirstOrDefaultAsync(s => s.StudentID == studentId);

      if (course == null || student == null)
      {
        return NotFound();
      }

      if (!course.IsOpenToEnroll)
      {
        TempData["Error"] = "Course is not open for enrollment.";
        return RedirectToAction(nameof(CourseManage), new { id = courseId });
      }

      if (_studentService.IsStudentEligibleToEnroll(student))
      {
        student.Courses.Add(course);
        _studentService.UpdateStudentStatus(student);
        await _context.SaveChangesAsync();
      }

      return RedirectToAction(nameof(CourseManage), new { id = courseId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DropStudent(int courseId, int studentId)
    {
      var course = await _context.Courses.FindAsync(courseId);
      var student = await _context.Students
          .Include(s => s.Courses)
          .FirstOrDefaultAsync(s => s.StudentID == studentId);

      if (course == null || student == null)
      {
        return NotFound();
      }

      student.Courses.Remove(course);
      _studentService.UpdateStudentStatus(student);
      await _context.SaveChangesAsync();

      return RedirectToAction(nameof(CourseManage), new { id = courseId });
    }

    // ========================================
    // StudentAccount
    public async Task<IActionResult> StudentAccount(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var student = await _context.Students
          .Include(s => s.FinancialStatement)
              .ThenInclude(fs => fs.FeePolicy)
          .Include(s => s.FinancialStatement)
              .ThenInclude(fs => fs.Entries)
          .FirstOrDefaultAsync(s => s.StudentID == id);

      if (student == null)
      {
        return NotFound();
      }

      // Store the course ID to return back to course page
      ViewBag.CourseId = TempData["CourseId"];

      return View(student);
    }
  }
}
