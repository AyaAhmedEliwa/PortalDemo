using Microsoft.AspNetCore.Mvc;
using Portal.Core.Model;
using Portal.Core.Reposatory;
using Portal.Core.Interface;
using AutoMapper;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;
using Portal.Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Portal.Presentation.Controllers
{
    [Authorize]
    public class DepartmentController : Controller 
    {
        private readonly IDepartmentRep departmentRep;
        private readonly IMapper mapper;


        //tightly coupling
        //DepartmentRep departmentRep;

        //loosely coupling
        //private readonly IDepartmentRep departmentRep;

        public DepartmentController(IDepartmentRep departmentRep, IMapper mapper)
        {
            this.departmentRep = departmentRep;
            this.mapper = mapper;
        }

        #region Main (index)
        public async Task <IActionResult> Index()
        {

            var dpts = await departmentRep.GetAllAsync();
            var data = mapper.Map<IEnumerable<DepartmentVM>>(dpts);

            //view data --->
            //  - refrence type like object
            //  - but.. need casting if i need to make operation in its data

            //ViewData["x"] = "Hi I'm View Data.";

            //view Bag --->
            //  - refrence type like object
            //  - but.. not need casting (is dynamic)

            //ViewBag.x = "Hi I'm View Bag.";

            //TempData["x"] = "Hi I'm Temp Data.";

            //string[] Names = new string[]{"Aya","Omar","Mohamed"};

            //ViewBag.names = Names;

            return View(data);
        }
        #endregion

        //Tag helper want to find 2 functions with same name (get , post)

        #region Department Details
        public async Task<IActionResult> Details(int id)
        {
            var dpt = await departmentRep.GetByIdAsync(id);
            var data = mapper.Map<DepartmentVM>(dpt); 
            return View(data);
        }
        #endregion

        #region Department Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Create(DepartmentVM department)
        {
            try
            {
                if(!ModelState.IsValid)
                    return View(department);

                var data = mapper.Map<Department>(department);
                await departmentRep.CreateAsync(data);                
                //ModelState.Clear();                
                
            }
            catch (Exception ex)
            {
                TempData["Erorr"] = "";
            }

            return RedirectToAction("Index", "Department");
        }
        #endregion

        #region Department Update

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var dpt = await departmentRep.GetByIdAsync(id);
            var data = mapper.Map<DepartmentVM>(dpt);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Update(DepartmentVM department)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(department);

                var data = mapper.Map<Department>(department);
                await departmentRep.UpdateAsync(data);
                //ModelState.Clear();                
            }
            catch (Exception ex)
            {
                TempData["Erorr"] = "";
            }
            return RedirectToAction("Index", "Department");
        }
        #endregion

        #region  Department Delete

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var dpt = await departmentRep.GetByIdAsync(id);
            var data = mapper.Map<DepartmentVM>(dpt);
            return View(data);
        }

        [HttpPost]
        [ActionName("Delete")] //if i want to change this fun. name 
        public async Task<IActionResult> Delete(DepartmentVM department)
        {
            try
            {
                var data = mapper.Map<Department>(department);
                await departmentRep.DeleteAsync(data);                               
            }
            catch (Exception ex)
            {
                TempData["Erorr"] = "";
            }
            return RedirectToAction("Index", "Department");
        }
        #endregion
    }
}
