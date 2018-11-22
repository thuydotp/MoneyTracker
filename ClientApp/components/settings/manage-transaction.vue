<template>
    <div>
        <h3>Manage Transaction</h3>
        <button type="button" class="btn btn-success" @click="addNewTransaction()">Create</button>
        <add-edit-transaction v-if="editedTransaction" 
                            :isEdit="isEditTransaction" :transaction="editedTransaction" 
                            @close-transaction="closeTransaction()"></add-edit-transaction>
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
import { TransactionService } from '../../store/api.js'
import TransactionList from "../shared/transaction-list";
import AddEditTransaction from "../shared/add-edit-transaction";
import modal from "../shared/modal";

export default {
  components: {
    'transaction-list': TransactionList,
    'add-edit-transaction': AddEditTransaction,
    modal: modal
  },
  data: function() {
    return {
      apiPath: `/api/Transaction`,
      isEditTransaction: false,  
      showModal: false,
      transactions: [],
      editedTransaction: null,
      listSpendingCategories: [],
      listSpendingAccounts: []
    };
  },
  methods: {
    addNewTransaction() {
      this.editedTransaction = {};
    },
    async createTransaction() {
      let response = await this.$http.post(this.apiPath, this.editedTransaction);      
      TransactionService.loadTransactions().then((data) => {
        this.transactions = data;
        this.editedTransaction = null;
        this.isEditTransaction = false;
      });
    },
    async updateTransaction() {
      let response = await this.$http.put(
        `${this.apiPath}/${this.editedTransaction.id}`,
        this.transaction
      );
      TransactionService.loadTransactions().then((data) => {
        this.transactions = data;
        this.editedTransaction = null;
        this.isEditTransaction = false;
      });
    },
    save() {
      if (this.isEditTransaction) {
        this.updateTransaction();
      } else {
        this.createTransaction();
      }
    },
    cancel() {
      this.editedTransaction = null;
      this.isEditTransaction = false;
    },
    editTransaction(transaction) {
      this.isEditTransaction = true;
      this.editedTransaction = Object.assign({}, transaction);
    },
    async deleteTransaction(id) {
      let response = await this.$http.delete(`${this.apiPath}/${id}`);      
      TransactionService.loadTransactions().then((data) => this.transactions = data);
    },
    closeTransaction() {
      TransactionService.loadTransactions().then((data) => {
        this.transactions = data;
        this.editedTransaction = null;
        this.showSummary = true;
      });
    }
  },
  async created() {
    TransactionService.loadTransactions().then((data) => this.transactions = data);
    TransactionService.loadCategories().then((data) => this.listSpendingCategories = data);
    TransactionService.loadAccounts().then((data) => this.listSpendingAccounts = data);
  }
};
</script>
