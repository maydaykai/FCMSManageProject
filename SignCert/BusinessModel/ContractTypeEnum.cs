namespace SignCert.BusinessModel
{
    public enum ContractTypeEnum
    {
        /// <summary>
        ///     借款及担保合同
        /// </summary>
        LoanGuaranteeContract = 1,

        /// <summary>
        ///     债权收益权转让及回购合同（典当）
        /// </summary>
        BuyBackContract,

        /// <summary>
        ///     债权转让合同
        /// </summary>
        CreditAssignment,
    }
}