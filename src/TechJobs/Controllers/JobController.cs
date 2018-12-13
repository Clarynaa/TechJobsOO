using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TechJobs.Data;
using TechJobs.Models;
using TechJobs.ViewModels;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            // TODO #1 -  DONE get the Job with the given ID and pass it into the view
            
            Job result = jobData.Find(id);

            return View(result);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.

            if (newJobViewModel != null && newJobViewModel.Name != null)
            {
                Employer newEmployer = jobData.Employers.Find(newJobViewModel.EmployerID);
                Location newLocation = jobData.Locations.Find(newJobViewModel.LocationID);
                CoreCompetency newCoreCompetency = jobData.CoreCompetencies.Find(newJobViewModel.CoreCompetencyID);
                PositionType newPosition = jobData.PositionTypes.Find(newJobViewModel.PositionTypeID);


                Job newJob = new Job {
                    Name = newJobViewModel.Name,
                    Employer = newEmployer,
                    Location = newLocation,
                    CoreCompetency = newCoreCompetency,
                    PositionType = newPosition
                };

                jobData.Jobs.Add(newJob);

                return Redirect(string.Format("Index/?id={0}", newJob.ID));
            }


            return View(newJobViewModel);
        }
    }
}
