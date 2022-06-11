using Application.Interfaces.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelPredictionController : ControllerBase
    {

        #region Fields

        private IModelPredictionBusiness _modelPredictionBusiness;

        #endregion

        #region Ctor

        public ModelPredictionController(IModelPredictionBusiness modelPredictionBusiness)
        {
            _modelPredictionBusiness = modelPredictionBusiness;
        }

        #endregion

        #region Methods

        [AllowAnonymous]
        [HttpGet]
        [Route("GetModelPrediction")]
        public IActionResult GetModelPrediction()
        {
            var result = _modelPredictionBusiness.GetLastModelPrediction().Result;
            return Ok(result);
        }
       
        #endregion
    }
}
