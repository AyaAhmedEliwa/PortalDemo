using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.Core.Helper;
using Portal.Core.Interface;
using Portal.Core.Model;
using Portal.Core.Reposatory;
using Portal.Infrastructure.Entities;
using Portal.Infrastructure.Migrations;

namespace Portal.Presentation.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {

        #region prop

        private readonly IEmployeeRep employeeRep;
        private readonly IDepartmentRep departmentRep;
        private readonly IMapper mapper;
        private readonly ICountryRep countryRep;
        private readonly ICityRep cityRep;
        private readonly IDistrictRep districtRep;

        #endregion

        #region ctor
        public EmployeeController(IEmployeeRep employeeRep, IDepartmentRep departmentRep, IMapper mapper,
            ICountryRep countryRep, ICityRep cityRep, IDistrictRep districtRep)
        {
            this.employeeRep = employeeRep;
            this.departmentRep = departmentRep;
            this.mapper = mapper;
            this.countryRep = countryRep;
            this.cityRep = cityRep;
            this.districtRep = districtRep;
        }
        #endregion

        #region Actions

        public async Task<IActionResult> Index(string MyName)
        {
            if (MyName != null)
            {
                var emps = await employeeRep.GetAllAsync(emp => emp.IsActive == true && emp.IsDeleted == false && emp.Name.Contains(MyName));
                var data = mapper.Map<IEnumerable<EmployeeVM>>(emps);
                return View(data);
            }
            else
            {
                var emps = await employeeRep.GetAllAsync(emp => emp.IsActive == true && emp.IsDeleted == false);
                var data = mapper.Map<IEnumerable<EmployeeVM>>(emps);
                return View(data);
            }
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var emp = await employeeRep.GetByIdAsync(emp => emp.Id == id && emp.IsActive == true && emp.IsDeleted == false);
            var data = mapper.Map<EmployeeVM>(emp);
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.DepartmentList = new SelectList(await departmentRep.GetAllAsync(), "Id", "Name");
            ViewBag.CountryList = new SelectList(await countryRep.GetAllAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeVM model)
        {
            try
            {                

                model.ImageName = FileUploader.UploadFile("Images", model.Image);
                model.CVName = FileUploader.UploadFile("CVs", model.CV);

                ViewBag.DepartmentList = new SelectList(await departmentRep.GetAllAsync(), "Id", "Name");

                if (!ModelState.IsValid)
                    return View(model);
                
                var data = mapper.Map<Employee>(model);
                await employeeRep.CreateAsync(data);
            }
            catch (Exception ex)
            {
                // Handle
                TempData["Error"] = "";
                // Log
            }
            return RedirectToAction("Index", "Employee");
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var emp = await employeeRep.GetByIdAsync(emp => emp.Id == id && emp.IsDeleted == false && emp.IsActive == true);
            var data = mapper.Map<EmployeeVM>(emp);

            ViewBag.DepartmentList = new SelectList(await departmentRep.GetAllAsync(), "Id", "Name", data.DepartmentId);

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EmployeeVM model)
        {
            try
            {
                // Get Employee
                ViewBag.DepartmentList = new SelectList(await departmentRep.GetAllAsync(), "Id", "Name", model.DepartmentId);

                if (!ModelState.IsValid)
                    return View(model);

                var data = mapper.Map<Employee>(model);
                await employeeRep.UpdateAsync(data);
            }
            catch (Exception ex)
            {
                // Handle
                TempData["Error"] = "";

                // Log
            }
            return RedirectToAction("Index", "Employee");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var emp = await employeeRep.GetByIdAsync(emp => emp.Id == id && emp.IsActive == true && emp.IsDeleted == false);
            var data = mapper.Map<EmployeeVM>(emp);
            return View(data);
        }

        [HttpPost]
        [ActionName("Delete")] //if i want to change this fun. name 
        public async Task<IActionResult> Delete(EmployeeVM model)
        {
            try
            {
                FileUploader.RemoveFile("Images", model.ImageName);
                FileUploader.RemoveFile("CVs", model.CVName);
                var data = mapper.Map<Employee>(model);
                await employeeRep.DeleteAsync(data);
            }
            catch (Exception ex)
            {
                TempData["Erorr"] = "";
            }
            return RedirectToAction("Index", "Employee");
        }
        #endregion

        #region Ajax Call

        [HttpPost]
        public async Task<JsonResult> GetCityByCountryId(int cntryId)
        {
            var data = await cityRep.GetAllAsync(cityRep => cityRep.CountryId == cntryId);
            return Json(data);
        }
        public async Task<JsonResult> GetDistrictByCityId(int cityId)
        {
            var data = await districtRep.GetAllAsync(districtRep => districtRep.CityId == cityId);
            return Json(data);
        }
        #endregion        
    }
}
