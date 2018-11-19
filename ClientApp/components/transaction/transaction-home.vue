<template>
    <div v-if="isLoaded">
      <div class="page-header">
        <h1>Transaction Page</h1>      
      </div>
      <div class="filter-container">
        <div>
          <b>Acccount:</b>
          <select v-model="selection.spendingAccountID">
            <option :value="nullValue">All</option>
              <option v-for="acc in listSpendingAccounts" :key="acc.id" :value="acc.id">{{acc.accountName}}</option>
          </select>
        </div>          
        <button class="btn btn-info" @click="selection.showTransactionDetails = !selection.showTransactionDetails">{{selection.showTransactionDetails ? "Show Summary": "View Details"}}</button>
      </div>        
      <transaction-summary v-if="!selection.showTransactionDetails" :transactions="filteredTransactions" @create-transaction="createTransaction"></transaction-summary>
      <transaction-list v-if="selection.showTransactionDetails" :transactions="filteredTransactions" 
                        @edit-transaction="editTransaction"
                        @delete-transaction="deleteTransaction"></transaction-list>        

      <modal v-if="showModal" @close="showModal = false">
        <template slot="modal-body-slot">            
          <add-edit-transaction :isEdit="isEditTransaction" :transaction="editedTransaction" 
                              @close-transaction="closeTransaction()"></add-edit-transaction>
        </template>
      </modal>
    </div>
</template>

<script>
import TransactionSummary from "./transaction-summary";
import AddEditTransaction from "../shared/add-edit-transaction";
import TransactionList from "../shared/transaction-list";
import modal from "../shared/modal";

export default {
  components: {
    "transaction-summary": TransactionSummary,
    "transaction-list": TransactionList,
    "add-edit-transaction": AddEditTransaction,
    modal: modal
  },
  data: function() {
    return {
      apiPath: `/api/SpendingItem`,
      isLoaded: false,
      editedTransaction: null,
      isEditTransaction: false,
      transactionType: null,
      transactions: [],
      showModal: false,
      listSpendingAccounts: [],
      selection: {
        spendingAccountID: null,
        showTransactionDetails: false
      },
      nullValue: null
    };
  },
  computed: {
    filteredTransactions() {
      let ignoreFilter = !this.selection.spendingAccountID;
      return this.transactions.filter(x => ignoreFilter || x.spendingAccountID === this.selection.spendingAccountID);
    }
  },
  methods: {
    async loadTransactions() {
      let response = await this.$http.get(this.apiPath);
      this.transactions = response.data;
    },
    createTransaction(transactionType) {
      this.isEditTransaction = false;
      this.editedTransaction = { type: transactionType, spendingAccountID: this.selection.spendingAccountID };
      this.showModal = true;
    },
    editTransaction(transaction) {
      this.isEditTransaction = true;
      this.editedTransaction = Object.assign({}, transaction);
      this.showModal = true;
    },
    async deleteTransaction(id) {
      let response = await this.$http.delete(`${this.apiPath}/${id}`);
      this.loadTransactions();
    },
    closeTransaction() {
      this.loadTransactions();
      this.editedTransaction = null;
      this.showModal = false;
    },
    async loadAccounts() {
      let response = await this.$http.get(`/api/SpendingAccount`);
      this.listSpendingAccounts = response.data;
    }
  },
  created() {
    Promise.all([this.loadTransactions(), this.loadAccounts()]).then(() => {
      this.isLoaded = true;
    });
  }
};
</script>

<style>
.filter-container {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding-right: 50px;
}
</style>
