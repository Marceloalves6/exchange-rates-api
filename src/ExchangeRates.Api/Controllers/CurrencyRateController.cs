using ExchangeRates.Api.Application.Contracts;
using ExchangeRates.Core.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace ExchangeRates.Api.Controllers;
/// <summary>
/// Exchange rate API
/// </summary>

[Route("api/v1/[controller]")]
[ApiController]
public class CurrencyRateController(IMediator mediator, IHttpContextAccessor httpContextAccessor) : BaseApiController<CurrencyRateController>(mediator, httpContextAccessor)
{
    /// <summary>
    /// Get the exchange rate of a currency pair
    /// </summary>
    /// <param name="currencyFrom">currencyFrom in ISO format</param>
    /// <param name="currencyTo">currencyTo in ISO format</param>
    /// <param name="cancellationToken"></param>
    /// <returns>An object containing the information about the exechange rate</returns>
    /// <remarks>
    /// In order to obtain the exchange rate of a currency pair, it is necessary to provide the ISO code corresponding to the given currencies.
    /// Here is a list of currencies ISO code:
    /// 
    ///  AED - United Arab Emirates Dirham, 
    ///  AFN - Afghan Afghani, 
    ///  ALL - Albanian Lek, 
    ///  AMD - Armenian Dram, 
    ///  ANG - Netherlands Antillean Guilder, 
    ///  AOA - Angolan Kwanza, 
    ///  ARS - Argentine Peso, 
    ///  AUD - Australian Dollar, 
    ///  AWG - Aruban Florin, 
    ///  AZN - Azerbaijani Manat, 
    ///  BAM - Bosnia-Herzegovina Convertible Mark, 
    ///  BBD - Barbadian Dollar, 
    ///  BDT - Bangladeshi Taka, 
    ///  BGN - Bulgarian Lev, 
    ///  BHD - Bahraini Dinar, 
    ///  BIF - Burundian Franc, 
    ///  BMD - Bermudan Dollar, 
    ///  BND - Brunei Dollar, 
    ///  BOB - Bolivian Boliviano, 
    ///  BRL - Brazilian Real, 
    ///  BSD - Bahamian Dollar, 
    ///  BTN - Bhutanese Ngultrum, 
    ///  BWP - Botswanan Pula, 
    ///  BZD - Belize Dollar, 
    ///  CAD - Canadian Dollar, 
    ///  CDF - Congolese Franc, 
    ///  CHF - Swiss Franc, 
    ///  CLF - Chilean Unit of Account UF, 
    ///  CLP - Chilean Peso, 
    ///  CNH - Chinese Yuan Offshore, 
    ///  CNY - Chinese Yuan, 
    ///  COP - Colombian Peso, 
    ///  CUP - Cuban Peso, 
    ///  CVE - Cape Verdean Escudo, 
    ///  CZK - Czech Republic Koruna, 
    ///  DJF - Djiboutian Franc, 
    ///  DKK - Danish Krone, 
    ///  DOP - Dominican Peso, 
    ///  DZD - Algerian Dinar, 
    ///  EGP - Egyptian Pound, 
    ///  ERN - Eritrean Nakfa, 
    ///  ETB - Ethiopian Birr, 
    ///  EUR - Euro, 
    ///  FJD - Fijian Dollar, 
    ///  FKP - Falkland Islands Pound, 
    ///  GBP - British Pound Sterling, 
    ///  GEL - Georgian Lari, 
    ///  GHS - Ghanaian Cedi, 
    ///  GIP - Gibraltar Pound, 
    ///  GMD - Gambian Dalasi, 
    ///  GNF - Guinean Franc, 
    ///  GTQ - Guatemalan Quetzal, 
    ///  GYD - Guyanaese Dollar, 
    ///  HKD - Hong Kong Dollar, 
    ///  HNL - Honduran Lempira, 
    ///  HRK - Croatian Kuna, 
    ///  HTG - Haitian Gourde, 
    ///  HUF - Hungarian Forint, 
    ///  ICP - Internet Computer, 
    ///  IDR - Indonesian Rupiah, 
    ///  ILS - Israeli New Sheqel, 
    ///  INR - Indian Rupee, 
    ///  IQD - Iraqi Dinar, 
    ///  IRR - Iranian Rial, 
    ///  ISK - Icelandic Krona, 
    ///  JEP - Jersey Pound, 
    ///  JMD - Jamaican Dollar, 
    ///  JOD - Jordanian Dinar, 
    ///  JPY - Japanese Yen, 
    ///  KES - Kenyan Shilling, 
    ///  KGS - Kyrgystani Som, 
    ///  KHR - Cambodian Riel, 
    ///  KMF - Comorian Franc, 
    ///  KPW - North Korean Won, 
    ///  KRW - South Korean Won, 
    ///  KWD - Kuwaiti Dinar, 
    ///  KYD - Cayman Islands Dollar, 
    ///  KZT - Kazakhstani Tenge, 
    ///  LAK - Laotian Kip, 
    ///  LBP - Lebanese Pound, 
    ///  LKR - Sri Lankan Rupee, 
    ///  LRD - Liberian Dollar, 
    ///  LSL - Lesotho Loti, 
    ///  LYD - Libyan Dinar, 
    ///  MAD - Moroccan Dirham, 
    ///  MDL - Moldovan Leu, 
    ///  MGA - Malagasy Ariary, 
    ///  MKD - Macedonian Denar, 
    ///  MMK - Myanma Kyat, 
    ///  MNT - Mongolian Tugrik, 
    ///  MOP - Macanese Pataca, 
    ///  MRO - Mauritanian Ouguiya (pre-2018), 
    ///  MRU - Mauritanian Ouguiya, 
    ///  MUR - Mauritian Rupee, 
    ///  MVR - Maldivian Rufiyaa, 
    ///  MWK - Malawian Kwacha, 
    ///  MXN - Mexican Peso, 
    ///  MYR - Malaysian Ringgit, 
    ///  MZN - Mozambican Metical, 
    ///  NAD - Namibian Dollar, 
    ///  NGN - Nigerian Naira, 
    ///  NOK - Norwegian Krone, 
    ///  NPR - Nepalese Rupee, 
    ///  NZD - New Zealand Dollar, 
    ///  OMR - Omani Rial, 
    ///  PAB - Panamanian Balboa, 
    ///  PEN - Peruvian Nuevo Sol, 
    ///  PGK - Papua New Guinean Kina, 
    ///  PHP - Philippine Peso, 
    ///  PKR - Pakistani Rupee, 
    ///  PLN - Polish Zloty, 
    ///  PYG - Paraguayan Guarani, 
    ///  QAR - Qatari Rial, 
    ///  RON - Romanian Leu, 
    ///  RSD - Serbian Dinar, 
    ///  RUB - Russian Ruble, 
    ///  RUR - Old Russian Ruble, 
    ///  RWF - Rwandan Franc, 
    ///  SAR - Saudi Riyal, 
    ///  SBDf - Solomon Islands Dollar, 
    ///  SCR - Seychellois Rupee, 
    ///  SDG - Sudanese Pound, 
    ///  SDR - Special Drawing Rights, 
    ///  SEK - Swedish Krona, 
    ///  SGD - Singapore Dollar, 
    ///  SHP - Saint Helena Pound, 
    ///  SLL - Sierra Leonean Leone, 
    ///  SOS - Somali Shilling, 
    ///  SRD - Surinamese Dollar, 
    ///  SYP - Syrian Pound, 
    ///  SZL - Swazi Lilangeni, 
    ///  THB - Thai Baht, 
    ///  TJS - Tajikistani Somoni, 
    ///  TMT - Turkmenistani Manat, 
    ///  TND - Tunisian Dinar, 
    ///  TOP - Tongan Pa'anga, 
    ///  TRY - Turkish Lira, 
    ///  TTD - Trinidad and Tobago Dollar, 
    ///  TWD - New Taiwan Dollar, 
    ///  TZS - Tanzanian Shilling, 
    ///  UAH - Ukrainian Hryvnia, 
    ///  UGX - Ugandan Shilling, 
    ///  USD - United States Dollar, 
    ///  UYU - Uruguayan Peso, 
    ///  UZS - Uzbekistan Som, 
    ///  VND - Vietnamese Dong, 
    ///  VUV - Vanuatu Vatu, 
    ///  WST - Samoan Tala, 
    ///  XAF - CFA Franc BEAC, 
    ///  XCD - East Caribbean Dollar, 
    ///  XDR - Special Drawing Rights, 
    ///  XOF - CFA Franc BCEAO, 
    ///  XPF - CFP Franc, 
    ///  YER - Yemeni Rial, 
    ///  ZAR - South African Rand, 
    ///  ZMW - Zambian Kwacha, 
    ///  ZWL - Zimbabwean Dollar.
    ///  
    /// Example:
    ///
    ///     POST api/v1/CurrencyRate/GetExchangeRate
    ///     {
    ///        "currencyFrom": "EUR",
    ///        "currencyTo": "USD"
    ///     }
    ///     
    /// </remarks>

    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(ServiceResponse<GetExchangeRateResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<NoResult>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceResponse<NoResult>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetExchangeRate([FromQuery] string? currencyFrom, [FromQuery] string? currencyTo, CancellationToken cancellationToken)
    {

        var response = await SendAsync(new GetExchangeRateCommand(new (currencyFrom, currencyTo)), cancellationToken);

        return Ok(response);
    }


    /// <summary>
    /// Create a exchange rate record for a currency pair
    /// </summary>
    /// <param name="resquest">The currency pair as well as the ask price and the bid price</param>
    /// <param name="cancellationToken"></param>
    /// <returns>An object containing the information about the exchange rate created for the pair of currencies</returns>
    /// <remarks>
    /// 
    /// NOTE: the fields currencyFrom and currencyTo must be a valid currency iso code.
    /// 
    /// Example:
    /// 
    ///     POST api/v1/CurrencyRate
    ///     {
    ///        "currencyFrom": "EUR",
    ///        "currencyTo": "USD",
    ///        "bidPrice": 1.07335000,
    ///        "askPrice": 1.07344000
    ///     }
    /// </remarks>

    [HttpPost]
    [Route("")]
    [ProducesResponseType(typeof(ServiceResponse<AddExchangeRateCommand>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<NoResult>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceResponse<NoResult>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] AddExchangeRateRequest resquest, CancellationToken cancellationToken)
    {
        var response = await SendAsync(new AddExchangeRateCommand(resquest), cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Update the information releted to given exchange rate
    /// </summary>
    /// <param name="resquest">The exchange rate information tha must be updated</param>
    /// <param name="cancellationToken"></param>
    /// <returns>An object that indicates if the operation was successfully executed as well as the exchange rate information</returns>
    /// /// <remarks>
    /// 
    /// NOTE: the fields currencyFrom and currencyTo must be a valid currency iso code.
    /// 
    /// Example:
    /// 
    ///     POST api/v1/CurrencyRate
    ///     {
    ///        "id" : "6a20ad9b-5f08-4c6a-8bd4-ea7c77c711a8",
    ///        "currencyFrom": "EUR",
    ///        "currencyTo": "USD",
    ///        "bidPrice": 1.07335000,
    ///        "askPrice": 1.07514200
    ///     }
    /// </remarks>
    [HttpPut]
    [Route("")]
    [ProducesResponseType(typeof(ServiceResponse<UpdateExchangeRateResquest>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<NoResult>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceResponse<NoResult>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] UpdateExchangeRateResquest resquest, CancellationToken cancellationToken)
    {
        var response = await SendAsync(new UpdateExchangeRateCommand(resquest), cancellationToken);

        return Ok(response);
    }
    /// <summary>
    /// Delete a exchange rate by providing the corresponding Id
    /// </summary>
    /// <param name="id">Exchange rate id</param>
    /// <param name="hardDelete">Indicates it must perform a hard or soft delete</param>
    /// <returns>An object that indicates if the operation was successfully executed</returns>
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(typeof(ServiceResponse<NoResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<NoResult>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceResponse<NoResult>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, [FromQuery] bool? hardDelete, CancellationToken cancellationToken)
    {
        var response = await SendAsync(new DeleteExchangeRateCommand(id, hardDelete.GetValueOrDefault(false)), cancellationToken);

        return Ok(response);
    }
}