import axios from 'axios'

const TransactionService = {
  $http: axios,
  loadCategories: async function () {
    let response = await this.$http.get(`/api/Category`)
    return response.data
  },
  loadAccounts: async function () {
    let response = await this.$http.get(`/api/SpendingAccount`)
    return response.data
  },
  loadTransactions: async function () {
    let response = await this.$http.get(`/api/Transaction`)
    return response.data
  },
  getDisplayedType: function (selectedType) {
    let type = parseInt(selectedType)
    if (type === 0) {
      return 'Expense'
    }
    if (type === 1) {
      return 'Income'
    }
    return 'Transaction'
  }
}

export {
  TransactionService
}
