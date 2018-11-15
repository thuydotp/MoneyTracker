<template>
    <div>      
        <h1>Transaction Page</h1>
        <transaction-summary v-if="showSummary" @create-transaction="createTransaction"></transaction-summary>
        <transaction-list v-if="showSummary" :transactions="transactions" 
                          @edit-transaction="editTransaction"
                          @delete-transaction="deleteTransaction"></transaction-list>
        <add-edit-transaction v-if="editedTransaction" 
                            :isEdit="isEditTransaction" :transaction="editedTransaction" 
                            @close-transaction="closeTransaction()"></add-edit-transaction>
    </div>
</template>

<script>
import TransactionSummary from "./transaction-summary";
import AddEditTransaction from "../shared/add-edit-transaction";
import TransactionList from "../shared/transaction-list";

export default {
  components: {
    "transaction-summary": TransactionSummary,
    "transaction-list": TransactionList,
    "add-edit-transaction": AddEditTransaction
  },
  data: function() {
    return {      
      apiPath: `/api/SpendingItem`,
      showSummary: true,
      editedTransaction: null,
      isEditTransaction: false,
      transactionType: null,
      transactions: []
    };
  },
  methods: {
    async loadTransactions() {
      let response = await this.$http.get(this.apiPath);
      this.transactions = response.data;
    },
    createTransaction(transactionType) {
      this.isEditTransaction = false;
      this.editedTransaction = { type: transactionType };
      this.showSummary = false;
    },
    editTransaction(transaction) {
      this.isEditTransaction = true;
      this.editedTransaction = Object.assign({}, transaction);
      this.showSummary = false;
    },    
    async deleteTransaction(id) {
      let response = await this.$http.delete(`${this.apiPath}/${id}`);
      this.loadTransactions();
    },
    closeTransaction() {
      this.loadTransactions();
      this.editedTransaction = null;
      this.showSummary = true;
    }
  },
  created() {
    this.loadTransactions();
  }
};
</script>
