<template>
    <div>
        <h3>{{transactionTitle}}</h3>
        <table class="table">
            <thead>
                <tr>
                <th scope="col">#</th>
                <th scope="col">Type</th>
                <th scope="col">Change Value</th>
                <th scope="col">Note</th>
                <th scope="col">Category</th>
                <th scope="col">Spending Account</th>
                <th scope="col">Date</th>
                <th scope="col"></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(trans, index) in transactions" :key="trans.id">
                <th scope="row">{{index}}</th>
                <td>{{trans.spendingTypeName}}</td>
                <td>{{trans.changeValue}}</td>
                <td>{{trans.description}}</td>
                <td>{{trans.categoryName}}</td>
                <td>{{trans.spendingAccountName}}</td>
                <td>{{trans.recordDate}}</td>
                <td>
                    <button type="button" class="btn btn-primary" @click="$emit('edit-transaction',trans)">Edit</button>
                    <button type="button" class="btn btn-danger" @click="$emit('delete-transaction',trans.id)">Delete</button>
                </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
export default {
  props: ["title", "transactions"],
  computed: {
    transactionTitle() {
      return this.title ? this.title : "Transactions";
    }
  }
};
</script>
