<template>
    <div>      
        <g-chart class="transaction-summary-chart"
          type="PieChart"
          :data="chartData"
          :options="chartOptions"
        />
        <div class="balance-container">          
            <span>Balance: </span><b>{{displayedBalance}}</b>
        </div>
              
        <div class="create-new-transaction">
            <button class="btn btn-danger round-button" @click="createTransaction(0)">Add Expense</button>
            <button class="btn btn-success round-button" @click="createTransaction(1)">Add Income</button>
        </div>
    </div>
</template>

<script>
import { GChart } from "vue-google-charts";

export default {
  components: {
    "g-chart": GChart
  },
  props: ["transactions"],
  data: function() {
    return {
      chartData: [],
      chartOptions: {
        title: "Transaction Summary",
        pieHole: 0.4,
        legend: "none"
      }
    };
  },
  computed: {
    displayedBalance() {
      let balance = 0;
      balance = this.transactions
        .map(x => {
          let factor = x.type == 0 ? -1 : 1;
          return x.changeValue * factor;
        })
        .reduce((sum, value) => {
          return (sum += value);
        }, 0);
      return balance;
    }
  },
  watch: {
    transactions: function (val) {
      this.transactions = val;
      this.populateChartData();
    },
  },
  methods: {
    createTransaction(transactionType) {
      this.$emit("create-transaction", transactionType);
    },
    getExpenseGroupByCategory() {
      let expenseCategories = [];
      let expenseItems = this.transactions.filter(x => x.type == 0);

      for (const key in expenseItems) {
        if (expenseItems.hasOwnProperty(key)) {
          const expense = expenseItems[key];
          let currentvalue = 0;
          let cateKey = expense.categoryName;
          let expenseCategory = expenseCategories[cateKey];
          currentvalue = expenseCategory ? expenseCategory.changeValue : 0;
          expenseCategories[cateKey] = currentvalue + expense.changeValue;
        }
      }
      return expenseCategories;
    },
    populateChartData() {
      let chartData = [];
      chartData.push(["Activity", "Dollar per period"]);

      let expenseCategories = this.getExpenseGroupByCategory();
      for (var categoryName in expenseCategories) {
        if (expenseCategories.hasOwnProperty(categoryName)) {
          let item = [categoryName, expenseCategories[categoryName]];
          chartData.push(item);
        }
      }
      this.chartData = chartData;
    }
  },
  async created() {
    this.populateChartData();
  }
};
</script>

<style scoped>
.transaction-summary-chart {
  display: flex;
  justify-content: space-between;
  width: 50%;
  margin: auto;
  height: 400px;
}

.balance-container {
  text-align: center;
  font-size: 30px;
}

.create-new-transaction {
  display: flex;
  justify-content: space-between;
  margin: auto;
  width: 80%;
  max-width: 400px;
}
</style>
