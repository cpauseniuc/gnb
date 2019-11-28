using AutoMapper;
using GNB.IBM.Api.ErrorHandling;
using GNB.IBM.Api.ViewModels;
using GNB.IBM.HerokuApp.Proxy;
using GNB.IBM.HerokuApp.Proxy.Entities;
using GNB.IBM.HerokuApp.Proxy.Enums;
using GNB.IBM.HerokuApp.Proxy.Infraestructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GNB.IBM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : BaseController
    {
        public TransactionController(DataManager dataManager, IMapper mapper) : base(dataManager, mapper) { }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var transactions = await _dataManager.GetTransactions();
                return new OkObjectResult(_autoMapper.Map<List<TransactionViewModel>>(transactions));
            }
            catch (ProxyException pex)
            {
                return BadRequest(new ApiError(pex.Message, pex));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(ex.Message, ex));
            }
        }

        [HttpGet("getTransactionsBySkuInEur")]
        public async Task<IActionResult> GetTransactionsBySkuInEur(string SKU, bool fromMemory = true)
        {
            return await GetTransactionReport(SKU, Currency.EUR, fromMemory);
        }

        [HttpGet("getTransactionReport")]
        public async Task<IActionResult> GetTransactionReport(string SKU, Currency currency, bool fromMemory = true)
        {
            try
            { 
                var rates = await _dataManager.GetRates(fromMemory);
                var transactions = await _dataManager.GetTransactions(fromMemory);
                if (!transactions.Any(tr => tr.SKU == SKU))
                    return BadRequest(new ErrorHandling.ApiError($"No transactions were found for SKU {SKU}",null));
                var transactionsBySkuViewModel = new TransactionsBySkuViewModel();
                transactionsBySkuViewModel.Currency = currency;
                foreach (var trans in transactions.Where(t => t.SKU == SKU))                
                    transactionsBySkuViewModel.Transactions.Add(
                        new TransactionViewModel
                        {
                            Amount = Math.Round((trans.Currency != currency) ? trans.GetAmountByCurrency(currency, rates) : trans.Amount, 2),
                            Currency = currency,
                            SKU = trans.SKU
                        });                
                transactionsBySkuViewModel.TotalAmount = Math.Round(transactionsBySkuViewModel.Transactions.Sum(t => t.Amount), 2);
                

                return new OkObjectResult(transactionsBySkuViewModel);
            }
            catch (ProxyException pex)
            {
                return BadRequest(new ApiError(pex.Message, pex));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(ex.Message, ex));
            }
        }
       

    }
}
