<template>
    <div>      
        <h1>Transaction Page</h1>       

        <transaction-summary @create-transaction="createTransaction"></transaction-summary>
        <div>
          <b>Balance: </b><span>{{displayedBalance}}</span>
        </div>
        <transaction-list :transactions="transactions" 
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
      editedTransaction: null,
      isEditTransaction: false,
      transactionType: null,
      transactions: [],
      showModal: false
    };
  },
  computed: {
    displayedBalance() {
      let balance = 0;
      balance = this.transactions
        .map(x => {
          let factor = 0;
          if (x.type == 0) {
            factor = -1;
          }
          if (x.type == 1) {
            factor = 1;
          }
          return x.changeValue * factor;
        })
        .reduce((sum, value) => {
          return (sum += value);
        }, 0);
      return balance;
    }
  },
  methods: {
    async loadTransactions() {
      let response = await this.$http.get(this.apiPath);
      this.transactions = response.data;
    },
    createTransaction(transactionType) {
      this.isEditTransaction = false;
      this.editedTransaction = { type: transactionType };
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
