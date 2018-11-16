<template>
    <div>      
        <h1>Transaction Page</h1>
        <modal v-if="showModal" @close="showModal = false">
          <template slot="modal-body-slot">            
            <add-edit-transaction :isEdit="isEditTransaction" :transaction="editedTransaction" 
                                @close-transaction="closeTransaction()"></add-edit-transaction>
          </template>
        </modal>

        <transaction-summary @create-transaction="createTransaction"></transaction-summary>
        <transaction-list :transactions="transactions" 
                          @edit-transaction="editTransaction"
                          @delete-transaction="deleteTransaction"></transaction-list>
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
    "modal": modal
  },
  data: function() {
    return {
      apiPath: `/api/SpendingItem`,
      editedTransaction: null,
      isEditTransaction: false,
      transactionType: null,
      transactions: [],
      showModal: false
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
      // this.showSummary = false;
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
    }
  },
  created() {
    this.loadTransactions();
  }
};
</script>
