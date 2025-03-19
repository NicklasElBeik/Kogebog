import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'views/welcome_view.dart';
import 'views/recipe_list_view.dart';
import 'views/recipe_detail_view.dart';
import 'views/recipe_form_view.dart';

class MyApp extends StatelessWidget {
  MyApp({super.key});

  final GoRouter _router = GoRouter(
    initialLocation: '/',
    routes: [
      GoRoute(path: '/', builder: (_, __) => WelcomeView()),
      GoRoute(path: '/recipes', builder: (_, __) => RecipeListView()),
      GoRoute(path: '/recipe/:id', builder: (context, state) {
        final id = state.pathParameters['id']!;
        return RecipeDetailView(recipeId: id);
      }),
      GoRoute(path: '/recipe/new', builder: (_, __) => RecipeFormView()),
    ],
  );

  @override
  Widget build(BuildContext context) {
    return MaterialApp.router(
      debugShowCheckedModeBanner: false,
      theme: ThemeData.dark(),
      routerConfig: _router,
    );
  }
}
