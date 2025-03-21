import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:kogebog_flutter/views/recipe_form_edit_view.dart';
import 'views/welcome_view.dart';
import 'views/recipe_list_view.dart';
import 'views/recipe_detail_view.dart';
import 'views/recipe_form_view.dart';

class MyApp extends StatelessWidget {
  MyApp({super.key});

  final Color primaryAccent = const Color(0xFF1e453e);

  final GoRouter _router = GoRouter(
    initialLocation: '/',
    routes: [
      GoRoute(path: '/', builder: (_, __) => WelcomeView()),
      GoRoute(path: '/recipes', builder: (_, __) => RecipeListView()),
      GoRoute(path: '/recipe/new', builder: (_, __) => RecipeFormView()),
      GoRoute(
        path: '/recipe/:id/edit',
        builder: (context, state) {
          final id = state.pathParameters['id']!;
          return RecipeFormEditView(recipeId: id);
        },
      ),
      GoRoute(
        path: '/recipe/:id',
        builder: (context, state) {
          final id = state.pathParameters['id']!;
          return RecipeDetailView(recipeId: id);
        },
      ),
    ],
  );

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: MaterialApp.router(
        debugShowCheckedModeBanner: false,
        theme: ThemeData(
          colorScheme: ColorScheme.fromSeed(
            seedColor: primaryAccent,
            brightness: Brightness.light,
          ),
          scaffoldBackgroundColor: const Color(0xFFEFEFEF),
          textTheme: GoogleFonts.sourceSans3TextTheme().copyWith(
            bodyMedium: GoogleFonts.sourceSans3(fontSize: 14, fontWeight: FontWeight.w500),
            bodyLarge: GoogleFonts.sourceSans3(fontSize: 16, fontWeight:  FontWeight.w600),
            titleLarge: GoogleFonts.sourceSans3(fontWeight: FontWeight.bold),
          ),
          inputDecorationTheme: InputDecorationTheme(
            focusedBorder: UnderlineInputBorder(
              borderSide: BorderSide(
                color: primaryAccent,
              ),
            ),
            enabledBorder: UnderlineInputBorder(
              borderSide: BorderSide(
                color: primaryAccent,
              ),
            ),
            border: UnderlineInputBorder(
              borderSide: BorderSide(
                color: primaryAccent,
              ),
            ),
          ),
        ),
        routerConfig: _router,
      ),
    );
  }
}
