using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using ClassLibrary_Lab4;

namespace Lab5.Controllers
{
    public class LabController : Controller
    {
        [Authorize]
        public IActionResult Lab1Proj()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Lab1Result(string inputFile, string outputFile)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(inputFile) || string.IsNullOrWhiteSpace(outputFile))
                {
                    throw new ArgumentException("Both input and output file paths must be provided.");
                }

                Lab1.GoLab1(inputFile, outputFile);
                ViewBag.Result = $"Results have been successfully saved to {outputFile}";
                ViewBag.Error = null;
            }
            catch (Exception ex)
            {
                ViewBag.Result = null;
                ViewBag.Error = ex.Message;
            }

            return View("Lab1Proj");
        }

        [Authorize]
        public IActionResult Lab2Proj()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Lab2Result(string inputFile, string outputFile)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(inputFile) || string.IsNullOrWhiteSpace(outputFile))
                {
                    throw new ArgumentException("Both input and output file paths must be provided.");
                }

                Lab2.GoLab2(inputFile, outputFile);
                ViewBag.Result = $"Results have been successfully saved to {outputFile}";
                ViewBag.Error = null;
            }
            catch (Exception ex)
            {
                ViewBag.Result = null;
                ViewBag.Error = ex.Message;
            }

            return View("Lab2Proj");
        }

        [Authorize]
        public IActionResult Lab3Proj()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Lab3Result(string inputFile, string outputFile)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(inputFile) || string.IsNullOrWhiteSpace(outputFile))
                {
                    throw new ArgumentException("Both input and output file paths must be provided.");
                }

                Lab3.GoLab3(inputFile, outputFile);
                ViewBag.Result = $"Results have been successfully saved to {outputFile}";
                ViewBag.Error = null;
            }
            catch (Exception ex)
            {
                ViewBag.Result = null;
                ViewBag.Error = ex.Message;
            }

            return View("Lab3Proj");
        }
    }
}
