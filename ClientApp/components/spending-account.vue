<template>
    <div>
        <h1>Manage Spending Account</h1>
        <button type="button" class="btn btn-success" @click="addNewAccount()">Create</button>
        <div v-if="spendingAccount">
            <div class="form-group">
              <label for="accountName">Account Name</label>
              <input type="text" class="form-control" id="accountName" placeholder="Insert account name here..." v-model="spendingAccount.accountName">
            </div>
            <button type="button" class="btn btn-info" @click="save()">Save</button>
            <button type="button" class="btn btn-light" @click="cancel()">Cancel</button>
        </div>
        <table class="table">
          <thead>
            <tr>
              <th scope="col">#</th>
              <th scope="col">Account Name</th>
              <th scope="col">Action</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(acc, index) in listSpendingAccounts" :key="acc.id">
              <th scope="row">{{index}}</th>
              <td>{{acc.accountName}}</td>
              <td>
                <button type="button" class="btn btn-primary" @click="editAccount(acc)">Edit</button>
                <button type="button" class="btn btn-danger" @click="deleteAccount(acc.id)">Delete</button>
              </td>
            </tr>
          </tbody>
        </table>
    </div>
</template>

<script>
export default {
  data() {
    return {
      apiPath: `/api/SpendingAccount`,
      listSpendingAccounts: null,
      spendingAccount: null,
      isEditMode: false
    };
  },

  methods: {
    async loadAccounts() {
      let response = await this.$http.get(this.apiPath);
      this.listSpendingAccounts = response.data;
    },

    addNewAccount() {
      this.spendingAccount = {};
    },

    async createAccount() {
      let response = await this.$http.post(this.apiPath, this.spendingAccount);
      this.loadAccounts();
      this.spendingAccount = null;
      this.isEditMode = false;
    },
    async updateAccount() {
      let response = await this.$http.put(
        `${this.apiPath}/${this.spendingAccount.id}`,
        this.spendingAccount
      );
      this.loadAccounts();
      this.spendingAccount = null;
      this.isEditMode = false;
    },
    save() {
      if (this.isEditMode) {
        this.updateAccount();
      } else {
        this.createAccount();
      }
    },
    cancel() {
      this.spendingAccount = null;
      this.isEditMode = false;
    },
    editAccount(account) {
      this.isEditMode = true;
      this.spendingAccount = Object.assign({}, account);
    },
    async deleteAccount(id) {
      let response = await this.$http.delete(`${this.apiPath}/${id}`);
      this.loadAccounts();
    }
  },

  async created() {
    this.loadAccounts();
  }
};
</script>
